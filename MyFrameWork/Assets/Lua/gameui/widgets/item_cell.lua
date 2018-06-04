ItemCell = ItemCell or BaseClass(BaseRender)

function ItemCell:__init()
	self.is_use_objpool = false

	if nil == self.root_node then
		-- 后期需修改
		self.ui_config = {"uis/widgets", "ItemCell"}
		local prefab = Resources.Load(self.ui_config[1].."/"..self.ui_config[2], typeof(GameObject))
		local u3dobj = U3DObject(GameObject.Instantiate(prefab))
		self:SetInstance(u3dobj)
		self.is_use_objpool = true
	end

	self.num = self:FindVariable("num")
end

function ItemCell:__delete()
	-- body
end

function ItemCell:Reset()
	self.root_node.rect.anchorMax = Vector2(0.5, 0.5)
	self.root_node.rect.anchorMin = Vector2(0.5, 0.5)
	self.root_node.rect.sizeDelta = Vector2(94, 95)
	self.root_node.rect.pivot = Vector2(0.5, 0.5)
	self.root_node.transform:SetLocalScale(1, 1, 1)
end

function ItemCell:SetNum(num)
	self.num:SetValue(num)
end