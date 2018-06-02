NetManager = NetManager or BaseClass()

function NetManager:__init()
	if NetManager.Instance ~= null then
		print_error("[NetManager] try to instance twice!")
	end
	NetManager.Instance = self

 	AddToUpdate(BindTool.Bind(self.Update, self))

 	GameNet.Instance:SetOnDisconnectCallBack(BindTool.Bind(self.OnDisconnect, self))
 	GameNet.Instance:SetOnSendErrorCallBack(BindTool.Bind(self.OnSendError, self))
 	GameNet.Instance:SetOnReConnectCallBack(BindTool.Bind(self.OnReConnect, self))
end

function NetManager:Update(time, time2)
	GameNet.Instance:Update()
end

function NetManager:__delete()
	GameNet.Instance:DisConnected()
	GameNet.Instance:Release()

	NetManager.Instance = nil
end

function NetManager:OnDisconnect()
	GlobalEventSystem.Fire(GameNetEvent.OnDisconnect, self)
end

function NetManager:OnSendError()
	GlobalEventSystem.Fire(GameNetEvent.OnSendError, self)
end

function NetManager:OnReConnect()
	GlobalEventSystem.Fire(GameNetEvent.OnReConnect, self)
end