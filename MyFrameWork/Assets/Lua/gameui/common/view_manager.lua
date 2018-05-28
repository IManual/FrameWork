ViewManager = ViewManager or BaseClass()

function ViewManager:__init()
	if nil ~= ViewManager.Instance then
		print_error("[ViewManager]:Attempt to create singleton twice!")
	end
	ViewManager.Instance = self

	self.view_list = {}				-- viewlist

	self.open_view_list = {}		-- { layer1 = {view1, view 2}, ... }

	self.wait_load_chat_list = {}
end

function ViewManager:__delete()
	ViewManager.Instance = nil
end

function ViewManager:DestoryAllAndClear()
	for k, v in pairs(self.view_list) do
		if v:IsOpen() then
			v:Close()
			v:Release()
		end
	end

	self.view_list = {}
	self.open_view_list = {}
	self.wait_load_chat_list = {}
end

-- 注册一个界面
function ViewManager:RegisterView(view, view_name)
	self.view_list[view_name] = view
end

-- 反注册一个界面
function ViewManager:UnRegisterview(view_name)
	self.view_list[view_name] = nil
end

-- 获取一个界面
function ViewManager:GetView(view_name)
	return self.view_list[view_name]
end

-- 界面是否打开
function ViewManager:IsOpen(view_name)
	if nil == self.view_list[view_name] then
		return false
	end
	return self.view_list[view_name]:IsOpen()
end

-- 是否有开启的界面
function ViewManager:HasOpenView()
	-- 查找normal层打开的所有界面
	local list = self.open_view_list[UiLayer.Normal]
	if nil == list then
		return false
	end

	for k,v in pairs(list) do
		if v.view_name and v.view_name ~= ViewName.MainUI and v.view_name ~= "" and v.active_close and v:IsRealOpen() then
			return true
		end
	end

	return false
end

--打开界面
local now_view = nil
-- view_name 界面；index 界面标签；key ;values
function ViewManager:Open(view_name, index, key, values)
	-- print_error("view_name >>>>>>>>>"..view_name)
	now_view = self.view_list[view_name]
	if nil ~= now_view then
		local is_open, tips = self:CheckShowUi(view_name, index)
		if is_open then
			now_view:Open(index)
			-- 分模块刷新界面
			if key ~= nil and values ~= nil then
				now_view:Flush(key, values)
			end
		else
			tips = (tips and tips ~= "" and tips) or Language.Common.FunOpenTip
			print_error(tips)
		end
	end
end

-- 关闭界面
function ViewManager:Close(view_name, ...)
	now_view = self.view_list[view_name]
	if nil ~= now_view then
		now_view:Close(...)
	end
end

-- 关闭所有界面
function ViewManager:CloseAll()
	for k, v in pairs(self.view_list) do
		if v:CanActiveClose() then
			-- 如果能主动关闭
			if v:IsOpen() then
				v:CLose()
			end
		end
	end
end

-- 关闭当前界面之外的所有界面
function ViewManager:CloseAllViewExceptViewName(view_name)
	for k, v in pairs(self.view_list) do
		if v:CanActiveClose() and k ~= view_name then
			if v:IsOpen() then
				v:Close()
			end
		end
	end
end

-- 是否可以显示改UI（预留给功能开启功能）  
function ViewManager:CheckShowUi(view_name, index)
	local can_show_view = true
	local tips = ""
	-- 如果在跨服
	if IS_ON_CROSSSERVER then
		if view_name then
			-- 判断跨服中是否可以打开 
		end
	end
	-- 判断界面是否满足功能开启
	if view_name and can_show_view then
		
	end

	-- 判断界面标签是否满足功能开启
	local can_show_index = true
	if index and can_show_index then
		
	end
	return can_show_view and can_show_index, tips
end

function ViewManager:FlushView(view_name, ...)
	now_view = self.view_list[view_name]
	if nil ~= now_view then
		now_view:Flush(...)
	end
end

function ViewManager:GetUiNode(view_name, node_name)
	now_view = self.view_list[view_name]
	if nil ~= now_view then
		return now_view:OnGetUiNode(node_name)
	end
	return nil
end

-- 添加新的界面 重新排层级
function ViewManager:AddOpenView(view)
	self:RemoveOpenView(view, true)
	self.open_view_list[view:GetLayer()] = self.open_view_list[view:GetLayer()] or {}
	table.insert(self.open_view_list[view:GetLayer()], view)

	self:SortView(view:GetLayer())
	self:CheckViewRendering()
	GlobalEventSystem.Fire(OtherEventType.VIEW_OPEN, view)
end

function ViewManager:RemoveOpenView(view, ignore)
	if ni == self.open_view_list[view:GetLayer()] then
		return
	end

	for k, v in ipairs(self.open_view_list[view:GetLayer()]) do
		if v == view then
			v.__sort_order__ = 0
			table.remove(self.open_view_list[view:GetLayer()], k)
			break
		end
	end
	if not ignore then
		self:CheckViewRendering()
	end
	GlobalEventSystem.Fire(OtherEventType.VIEW_CLOSE, view)
end

function ViewManager:CheckViewRendering()
	
end

local sort_interval = 10
function ViewManager:SortView(layer)
	if nil == self.open_view_list[layer] then
		return
	end

	for i, v in ipairs(self.open_view_list[layer]) do
		if v.__sort_order__ ~= i  then
			v.__sort_order__ = i
			-- local root = v:GetRootNode()
			-- if nil ~= root then
			-- 	local canvases = root:GetComponentsInChildren(typeof(CS.UnityEngine.Canvas), true)
			-- 	local canvas_len = canvases.Length
			-- 	for j = 0, canvas_len - 1 do
			-- 		local canvas = canvases[j]
			-- 		-- Dropdown会设置到30000， 默认是所有层级的最上面。
			-- 		-- 防止把Dropdown的行为改变了
			-- 		if canvas.sortingOrder < 30000 then
			-- 			canvas.overrideSorting = true
			-- 			canvas.sortingOrder = canvas.sortingOrder % sort_interval + layer * 1000 + i * sort_interval
			-- 		end
			-- 	end

			-- 	local overriders = root:GetComponentsInChildren(typeof(CS.SortingOrderOverrider))
			-- 	local overrider_len = overriders.Length
			-- 	for j = 0, overrider_len - 1 do
			-- 		local overrider = overriders[j]
			-- 		overrider.SortingOrder = overrider.SortingOrder % sort_interval + layer * 1000 + i * sort_interval
			-- 	end
			-- end
		end
	end
end