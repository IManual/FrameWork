KnapsackView = KnapsackView or BaseClass(BaseRender)

local ItemCellCount = 40
function KnapsackView:__init()
	self.contain_cell_list = {}
	self.list_view = self:FindObj("ListView")
	local list_simple_delegate = self.list_view.list_simple_delegate
	list_simple_delegate.NumberOfCellsDel = BindTool.Bind(self.NumberOfCellsDel, self)
	list_simple_delegate.CellRefreshDel = BindTool.Bind(self.CellRefreshDel, self)
end

function KnapsackView:__delete()
	-- body
end

function KnapsackView:NumberOfCellsDel()
	return ItemCellCount / 5
end

function KnapsackView:CellRefreshDel(cell, cell_index)
	local contain_cell = self.contain_cell_list[cell]
	if contain_cell == nil then
		contain_cell = KnapsackCell.New(cell.gameObject, self)
		contain_cell:SetChildrenToggeleGroup(self.list_view.toggle_group)
		self.contain_cell_list[cell] = contain_cell
	end

	cell_index = cell_index + 1
	contain_cell:SetIndex(cell_index)
	contain_cell:SetData({})
end

function KnapsackView:OpenCallBack()
	-- body
end

function KnapsackView:CloseCallBack()
	-- body
end

-----------------------------KnapsackCell---------------------------------
KnapsackCell = KnapsackCell or BaseClass(BaseCell)
function KnapsackCell:__init()
	self.item_cell_obj_list = {}
	self.item_cell_list = {}
	for i = 1, 5 do
		self.item_cell_obj_list[i] = self:FindObj("Item_"..i)
		local item_cell = ItemCell.New()
		self.item_cell_list[i] = item_cell
		self.item_cell_list[i]:SetInstanceParent(self.item_cell_obj_list[i])
		self.item_cell_list[i]:ListenClick(BindTool.Bind(self.Click, self, i))
	end

	self.index = 0
end

function KnapsackCell:__delete()
	self.item_cell_obj_list = {}

	for k,v in pairs(self.item_cell_list) do
		v:DeleteMe()
	end
	self.item_cell_list = {}
end

function KnapsackCell:OnFlush()
	self.index = self:GetIndex()
	for i = 1, 5 do
		self.item_cell_list[i]:SetNum(i * 5 - 5 + self.index)
	end
end

function KnapsackCell:Click(index)
	index = index * 5 - 5 + self.index
	print_error("Item_cell"..index)
end

function KnapsackCell:SetChildrenToggeleGroup(group)
	for i = 1, 5 do
		self.item_cell_list[i]:SetToggleGroup(group)
	end
end