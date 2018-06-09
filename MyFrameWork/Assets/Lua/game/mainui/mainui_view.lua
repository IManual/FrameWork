require("game/mainui/knapsack_view")
MainUIView = MainUIView or BaseClass(BaseView)

function MainUIView:__init()
	self.ui_config = {"uis/views/mainui","MainView"}
	self.full_screen = true
end

function MainUIView:__delete()
	
end

function MainUIView:LoadCallBack()
	print("LoadCallBack")
	self.tab1_content = self:FindObj("Tab1Content")
	self.tab2_content = self:FindObj("Tab2Content")
	self.tab3_content = self:FindObj("Tab3Content")

	self.tab_1 = self:FindObj("Tab1")
	self.tab_2 = self:FindObj("Tab2")
	self.tab_3 = self:FindObj("Tab3")

	test = {"dsdasd","dasdsadsad","32321","12321"}
	print_error(test)

	self.tab_1.toggle:AddValueChangedListener(BindTool.Bind(self.OnTabValueChange, self, TabIndex.Tab1))
	self.tab_2.toggle:AddValueChangedListener(BindTool.Bind(self.OnTabValueChange, self, TabIndex.Tab2))
	self.tab_3.toggle:AddValueChangedListener(BindTool.Bind(self.OnTabValueChange, self, TabIndex.Tab3))
end

-- 销毁前调用
function MainUIView:ReleaseCallBack()
	print("ReleaseCallBack")
	self.tab1_content = nil
	self.tab2_content = nil
	self.tab3_content = nil

	self.tab_1 = nil
	self.tab_2 = nil
	self.tab_3 = nil

	if self.knapsack_view then
		self.knapsack_view:DeleteMe()
	end
end

function MainUIView:OnTabValueChange(index, isOn)
	if not isOn then
		return
	end
	self:ChangeToIndex(index)
end

-- 切换标签调用
function MainUIView:ShowIndexCallBack(index)
	if index == TabIndex.Tab1 and not self.tab1_view then

	elseif index == TabIndex.Tab2 and not self.tab2_view then

	elseif index == TabIndex.Tab3 and not self.knapsack_view then
		self.tab3_content.uiprefab_loader:Wait(function(obj)
			obj = U3DObject(obj)
			self.knapsack_view = KnapsackView.New(obj)
			self.knapsack_view:OpenCallBack()
		end)
	end
end

function MainUIView:OpenCallBack()
	print("OpenCallBack")
	if self.tab_1.toggle.isOn then
		self:ChangeToIndex(TabIndex.Tab1)
	elseif self.tab_2.toggle.isOn then
		self:ChangeToIndex(TabIndex.Tab2)
	else
		self:ChangeToIndex(TabIndex.Tab3)
	end
end

-- 关闭前调用
function MainUIView:CloseCallBack()
	print("CloseCallBack")
end

function MainUIView:OnFlush(param_list)
	-- body
end
