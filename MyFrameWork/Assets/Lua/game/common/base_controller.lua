-- 协议处理类的基类
BaseController = BaseController or BaseClass()

function BaseController:__init()
	self.event_map = {}					-- 事件表
	self.msg_type_map = {}
end

function BaseController:__delete()
	for k, _ in pairs(self.event_map) do
		GloabEventSystem:UnBind(k)
	end
	self.event_map = {}

	for k, v in pairs(self.msg_type_map) do
		
	end
end

function BaseController:RegisterProtocol(protocol, func_name)
	if protocol == nil then
		print_error("Ther register Protocol is nil")
		return 
	end

	local msg_type = GameNet.Instance:Register(protocol)
	
end

function BaseController:BindGloabEvnet(eventType, func)
	
end

