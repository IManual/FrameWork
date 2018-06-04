BaseView = BaseView or BaseClass()

CloseMode = {
	CloseVisible = 1,		-- 隐藏
	CloseDestroy = 2,		-- 延时销毁
}

UiLayer = {
	SceneName = 0,			-- 场景名字
	FloatText = 1,			-- 飘字
	MainUILow = 2,			-- 主界面(低)
	MainUI = 3,				-- 主界面
	MainUIHeight = 4,		-- 主界面(高)
	Normal = 5,				-- 普通界面
	Pop = 6,				-- 弹出框
	PopTop = 7,				-- 弹出框(高)
	Guide = 8,  			-- 引导层
	SceneLoading = 9,		-- 场景加载层
	SceneLoadingPop = 10,	-- 场景加载层上的弹出层
	Disconnect = 11,		-- 断线面板弹出层
	Standby = 12,			-- 待机遮罩
	MaxLayer = 13,			-- 最上层
}

-- 界面缓存时间
ViewCacheTime = {
	LEAST = 5,
	NORMAL = 60,
	MOST = 3000,
}

local UIRoot = GameObject.Find("GameRoot/UILayer").transform
local total_pop_view_count = 0
local pop_view_stack = {}

function BaseView:__init(view_name)
	self.close_mode = CloseMode.CloseDestroy
	self.view_layer = UiLayer.Normal

 	self.ui_config = nil						-- {bundle_name, prefab_name}
 	self.ui_scene = nil							-- 是否有UI伴随场景
 	self.full_screen = false					-- 是否是全屏界面
 	self.view_cache_time = ViewCacheTime.LEAST	-- 界面缓存时间
 	self.is_async_load = true					-- 是否异步加载
 	self.is_check_reduce_men = false			-- 是否检查减少内存

 	self.active_close = true					-- 是否可以主动关闭(用于关闭所有界面操作)
 	self.fight_info_view = false

 	self.root_node = nil						-- UI根节点
 	self.name_table = nil						-- 名字绑定
 	self.event_table = nil
 	self.variable_table = nil
 	self.animator = nil

 	self.is_loading = false						-- 是否加载中
 	self.is_open = false						-- 是否已创建预制体
 	self.is_rendering = false					-- 是否渲染
 	self.is_real_open = false					-- 是否已打开并显示预制体

 	self.flush_param_t = nil 					-- 界面刷新参数

 	self.def_index = 0							-- 默认显示的标签
 	self.last_index = nil						-- 上次显示的标签
 	self.show_index = -1 						-- 当前显示的标签
 	self.pop_view_count = 0

 	-- self.audio_config = AudioData.Instance:GetAudioConfig()
 	-- if self.audio_config then
 	-- 	self.open_audio_id = AssetID("audios/sfxs/uis", self.audio_config.other[1].DefaultOpen)				-- 打开面板音效
		-- self.close_audio_id = AssetID("audios/sfxs/uis", self.audio_config.other[1].DefaultClose)				-- 关闭面板音效
 	-- end

 	if nil ~= view_name and "" ~= view_name then
 		self.view_name = view_name
 		ViewManager.Instance:RegisterView(self, view_name)
 	end
end

function BaseView:__delete()
	self:Release()
end

function BaseView:Release()
	self.is_loading = false
	if nil == self.root_node then
		return
	end

	self:CancelReleaseTimer()

	self:ReleaseCallBack()

	if not IsNil(self.event_table) then
		self.event_table:ClearAllEvent()
		self.event_table = nil
	end

	GameObject.Destroy(self.root_node)
	self.root_node = nil
 	self.variable_table = nil
 	self.animator = nil

 	self.last_index = nil
	self.show_index = -1

 	self.is_open = false
	self.is_rendering = false
	self.is_real_open = false
 	self.flush_param_t = nil

 	self:CancelDelayFlushTimer()

end

function BaseView:CancelReleaseTimer()
	if nil ~= self.release_timer then
		GlobalTimeRequest.CancleTime(self.release_timer)
		self.release_timer = nil
	end
end

-- 查找组件
-- name_path 对象名，支持name/name/name的形式
function BaseView:FindObj(name_path, component_type, no_print)
	if self.name_table ~= nil then
		local game_obj = self.name_table:FindObj(name_path)
		if game_obj ~= nil then
			node = U3DObject(game_obj)
			return node
		end
	end
	local transform = self.root_node.transform:FindHard(name_path)
	if transform ~= nil then
		node = U3DObject(transform.gameObject, transform)
		return node
	end
	if not no_print then
		print_error("BaseView: can not find: " .. name_path)
	end

	return nil
end

-- 监听事件
function BaseView:ListenEvent(eventName, listener)
	if self.event_table ~= nil then
		return self.event_table:ListenEvent(eventName, listener)
	end
	return
end

function BaseView:ClearEvent(eventName)
	if self.event_table ~= nil then
		return self.event_table:ClearEvent(eventName)
	end
	return
end

-- 查找指定的绑定变量
function BaseView:FindVariable(name)
	if self.variable_table ~= nil then
		return self.variable_table:FindVariable(name)
	end
	return
end

-- 实际打开场景 加载预制体 跳转到index标签
function BaseView:Open(index)
	self.is_real_open = true
	index = index or self.def_index
	self:CancelReleaseTimer()

	if self.is_check_reduce_men and CS.UnityEngine.SystemInfo.systemMemorySize <= 1500 then
		-- 系统减少内存
	end
	if not self.is_open then
		-- 如果界面未打开
		self:SetActive(true)
		-- 场景未创建 则加载
		if not self:IsLoaded() then
			self:Load(index)
		else
			if nil ~= self.root_node then
				if self.animator ~= nil then
					self.animator:SetBool(ANIMATOR_PARAM.SHOW, true)
				end
				self:UpdateSortOrder()
				self:ShowIndex(index)
				-- if self.open_audio_id and self.play_audio then
				-- 	AudioManager.PlayAndForget(self.open_audio_id)
				-- end
				self:AddPopView()
 				self:OpenCallBack()
				self:FlushHelper()
			end
		end
	else
		-- 已经打开则跳转标签
		self:ShowIndex(index)
	end
end

-- 预制体加载 加载完成调用PrefabLoadCallback
function BaseView:Load(index)
	if nil == self.ui_config or self:IsLoaded() or self.is_loading then
		return
	end

	self.is_loading = true
	local request_id = LoadingPriorityManager.Instance:RequestPriority(LoadingPriority.High)
	if self.is_async_load then
		-- 异步加载资源
		print_log(self.ui_config[1].."/"..self.ui_config[2])
		obj = Resources.Load(self.ui_config[1].."/"..self.ui_config[2], typeof(GameObject))
		self:PrefabLoadCallback(index, obj)
	else

	end
end

-- 预制体加载完成回调
function BaseView:PrefabLoadCallback(index, obj)
	if nil == obj or not self.is_loading then
		self.is_loading = false
		return
	end

	obj = GameObject.Instantiate(obj)
	obj.name = string.gsub(obj.name, "%(Clone%)", "")

	self.is_loading = false

	self.root_node = obj
	self.name_table = self.root_node:GetComponent(typeof(CS.NameTable))
	self.event_table = self.root_node:GetComponent(typeof(CS.EventTable))
	self.variable_table = self.root_node:GetComponent(typeof(CS.VariableTable))
	self.animator = self.root_node:GetComponent(typeof(CS.UnityEngine.Animator))

	local transform = self.root_node.transform
	transform:SetParent(UIRoot, false)

	self:LoadCallBack(0, 1)
	self:UpdateSortOrder()
	if self:IsOpen() then
		if self.animator ~= nil then
			self.animator:SetBool(ANIMATOR_PARAM.SHOW, true)
		end
		self:ShowIndex(index)
		-- if self.open_audio_id and self.play_audio then
		-- 	AudioManager.PlayAndForget(self.open_audio_id)
		-- end
		self:AddPopView()
		self:OpenCallBack()
		self:FlushHelper()
	else
		self:SetActive(false, true)
	end
end

function BaseView:ChangeToIndex(index)
	if not self:IsOpen() then
		return
	end

	self:ShowIndex(index)
	self:FlushHelper()
end

-- 切换标签
function BaseView:ShowIndex(index)
	if not self:IsLoaded() then
		return
	end

	if self.show_index == index then
		return
	end

	if nil == index then
		print_log("BaseView:ShowIndex index == nil")
		return
	end

	self.show_index = index
	self.last_index = index

	self:ShowIndexCallBack(index)
end

-- 初始化界面坐标 对已开启界面排序
function BaseView:UpdateSortOrder()
	if nil ~= self.root_node then
		-- 重置坐标位置
		local transform = self.root_node.transform
		transform:SetLocalScale(1, 1, 1)
		local rect = transform:GetComponent(typeof(CS.UnityEngine.RectTransform))
		rect.anchorMax = Vector2(1, 1)
		rect.anchorMin = Vector2(0, 0)
		rect.anchoredPosition3D = Vector3(0, 0, 0)
		rect.sizeDelta = Vector2(0, 0)
		-- 更新深度值
		ViewManager.Instance:AddOpenView(self)
	end
end

function BaseView:Close(...)
	self.is_real_open = false
	if not self.is_open then
		self:CloseDestroy()
		return
	end

	-- 将自身出栈
	if self.pop_view_count > 0 then
		self:ReducePopView()
	end
	self:CloseCallBack(...)

	if self.animator ~= nil and self.animator.isActiveAndEnabled and  self.is_rendering and self.animator:GetBoo("show") then
		self.is_rendering = false
		self.animator:SetBool("show", false)
		self.animator:WaitEvent("exit", function(param)
			if self.is_real_open then
				self.is_open = false
				self:Open()
			else
				if self.close_mode == CloseMode.CloseVisible then
					self:CloseVisible()
				elseif self.close_mode == CloseMode.CloseDestroy then
					self:CloseDestroy()
				end
			end
		end)
	else
		if self.close_mode == CloseMode.CloseVisible then
			self:CloseVisible()
		elseif self.close_mode == CloseMode.CloseDestroy then
			self:CloseDestroy()
		end
	end
end

-- 关闭为不可见
function BaseView:CloseVisible()
	self.is_real_open = false
	self.show_index = -1
	if self:IsOpen() then
		ViewManager.Instance:RemoveOpenView(self)
	end
	self:SetActive(false)
	self:CancelDelayFlushTimer()
end

-- 关闭延时销毁
function BaseView:CloseDestroy()
	-- 先隐藏
	self:CloseVisible()
	if nil == self.release_timer then
		self.release_timer = GlobalTimeRequest.AddDelayTime(self.view_cache_time,function()
			self.release_timer = nil
			self:Release()
		end)
	end 
	if self.full_screen then
		--UtilU3D.ForceReSetCamera()
	end
end

function BaseView:SetActive(active, force)
	if self.is_open ~= active or force then
		self.is_open = active
		self.is_rendering = active
		self:SetRootNodeActive(active)
	end
end

function BaseView:SetRootNodeActive(value)
	if nil ~= self.root_node then
		self.root_node:SetActive(value)
	end
end

function BaseView:SetRendering(value)
	if self.is_rendering ~= value then
		self.is_rendering = value
		self:SetRootNodeActive(value)
	end
end

-- 是否能主动关闭
function BaseView:CanActiveClose()
	return self.active_close
end

function BaseView:IsLoaded()
	return nil ~= self.root_node
end

-- 是否已经开启
function BaseView:IsOpen()
	return self.is_open
end

function BaseView:IRealOpen()
	return self.is_real_open
end

function BaseView:GetLayer()
	return self.view_layer
end

function BaseView:GetRootNode()
	return self.root_node
end

-- 查找UI节点
function BaseView:OnGetUiNode(node_name)
	return self:FindObj(node_name)
end

function BaseView:Flush(key, value_t)
	key = key or "all"
	value_t = value_t or {"all"}

	self.flush_param_t = self.flush_param_t or {}
	for k, v in pairs(value_t) do
		self.flush_param_t[key] = self.flush_param_t[key] or {}
		self.flush_param_t[key][k] = v
	end
	if nil == self.delay_flush_timer and self:IsLoaded() and self:IsOpen() then
		-- 做延时一帧处理
		self.delay_flush_timer = GlobalTimeRequest.AddDelayTime(0, BindTool.BindTool(self.FlushHelper, self))
	end
end

function BaseView:FlushHelper()
	self:CancelDelayFlushTimer()

	if not self:IsOpen() or not self:IsLoaded() then
		return
	end

	if nil ~= self.flush_param_t then
		local param_list =self.flush_param_t
		self.flush_param_t = nil
		self:OnFlush(param_list)
	end
end

function BaseView:CancelDelayFlushTimer()
	if self.delay_flush_timer ~= nil then
		GlobalTimeRequest:CancelQuest(self.delay_flush_timer)
		self.delay_flush_timer = nil
	end
end

function BaseView:GetViewName()
	return self.view_name or ""
end

function BaseView:GetShowIndex()
	return self.show_index
end

function BaseView:AddPopView()
	-- 如果不是全屏界面
	if self.full_screen == false then
		-- 找到背景图
		local bg = self:FindBg()
		if nil ~= bg and nil ~= bg.image then
			total_pop_view_count = total_pop_view_count + 1
			self.pop_view_count = total_pop_view_count
			table.insert(pop_view_stack, self)
			BaseView.CheckPopBg()
		end 
	end
end

function BaseView:ReducePopView()
	total_pop_view_count = math.max(total_pop_view_count - 1, 0)
	for i = self.pop_view_count + 1, #pop_view_stack do
		local view = pop_view_stack[i]
		if view then
			view.pop_view_count = view.pop_view_count - 1
		end
	end
	table.remove(pop_view_stack, self.pop_view_count)
	self.pop_view_count = 0
	local bg = self:FindBg()
	if nil ~= bg and nil ~= bg.image then
		bg.image.color = Color.New(bg.image.color.r, bg.image.color.b, bg.image.color.g, 0)
	end
	BaseView.CheckPopBg()
end

function BaseView:FindBg()
	local bg = self:FindObj("BGButton", nil, true)
	if nil == bg then
		bg = self:FindObj("BG", nil, true)
	end
	if nil == bg then
		bg = self:FindObj("Block", nil, true)
	end
	return bg
end

-- 多个小界面重叠 隐藏底层界面背景图
function BaseView.CheckPopBg()
	for k, v in ipairs(pop_view_stack) do
		local bg = v:FindBg()
		if nil ~= bg and nil ~= bg.image then
			if k == total_pop_view_count then
				bg.image.color = Color.New(bg.image.color.r, bg.image.color.b, bg.image.color.g, 0.75)
			else
				bg.image.color = Color.New(bg.image.color.r, bg.image.color.b, bg.image.color.g, 0)
			end
		end
	end
end

---------------------------------------------------------
-- 继承
---------------------------------------------------------

-- 创建完调用
function BaseView:LoadCallBack()
	-- body
end

function BaseView:OpenCallBack()
	-- body
end

-- 切换标签调用
function BaseView:ShowIndexCallBack(index)
	-- body
end

-- 关闭前调用
function BaseView:CloseCallBack()
	-- body
end

-- 销毁前调用
function BaseView:ReleaseCallBack()
	-- body
end

function BaseView:OnFlush(param_list)
	-- body
end
