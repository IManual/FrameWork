ItemCell = ItemCell or BaseClass(BaseRender)

function ItemCell:__init()
	self.is_use_objpool = false

	if nil == self.root_node then
		-- 后期需修改
		local prefab = AssetLoaderManager.LoadAsset("uis/widgets", "ItemCell", typeof(GameObject))
		local u3dobj = U3DObject(GameObject.Instantiate(prefab))
		self:SetInstance(u3dobj)
		self.is_use_objpool = true
	end

	self.num = self:FindVariable("num")
end

function ItemCell:__delete()
	-- body
end

function ItemCell:SetData(data)
	-- 每次SetData刷新点击事件
	if self.handler ~= nil then
		self:ListenClick()
	end
end

-- 外部调用 添加点击事件  不添加则选择默认事件
function ItemCell:ListenClick(handler)
	self:ClearEvent("Click")
	self.handler = handler
	self:ListenEvent("Click", handler or BindTool.Bind(self.OnClickItemCell, self))
end

-- 默认的点击事件
function ItemCell:OnClickItemCell()
	print("OnClickItemCell")
end

function ItemCell:CleraItemEvent()
	self:ClearEvent("Click")
end

function ItemCell:Reset()
	self.root_node.rect.anchorMax = Vector2(0.5, 0.5)
	self.root_node.rect.anchorMin = Vector2(0.5, 0.5)
	self.root_node.rect.sizeDelta = Vector2(94, 95)
	self.root_node.rect.pivot = Vector2(0.5, 0.5)
	self.root_node.transform:SetLocalScale(1, 1, 1)
end

function ItemCell:SetToggleGroup(toggle_group)
	if self.root_node.toggle and self:GetActive() then
		self.root_node.toggle.group = toggle_group
	end
end

function ItemCell:GetActive()
	if self.root_node.gameObject and not IsNil(self.root_node.gameObject) then
		return self.root_node.gameObject.activeSelf
	end
	return false
end

function ItemCell:SetNum(num)
	self.num:SetValue(num)
end