ModulesController = ModulesController or BaseClass()

function ModulesController:__init()
	if ModulesController.Instance ~= nil then
		print_error("[ModulesController]:Attempt to create singleton twice!")
	end
	ModulesController.Instance = self

	self:CreateCoreModule()

	self.ctrl_list = {}
	self:CreateGameModule()

end

function ModulesController:__delete()
	self:DeleteGameModule()
	self:DeleteCoreMoudle()

	ModulesController.Instance = nil
end

-- 加载管理类
function ModulesController:CreateCoreModule()
	NetManager.New()
	ViewManager.New()
end

function ModulesController:DeleteCoreMoudle()
	NetManager.Instance:DeleteMe()
	ViewManager.Instance:DeleteMe()
end

-- 创建Ctrl
function ModulesController:CreateGameModule()
	self.ctrl_list = {}

	table.insert(self.ctrl_list, MainUICtrl.New())
end

-- 删除Ctrl
function ModulesController:DeleteGameModule()
	local count = #self.ctrl_list
	for i = count, 1, -1 do
		self.ctrl_list[i]:DeleteMe()
	end
	self.ctrl_list = {}
end