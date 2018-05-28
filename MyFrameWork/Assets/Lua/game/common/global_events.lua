-- 全局事件定义
SystemEventType =
{
	GAME_PAUSE = "game_pause",
	GAME_FOCUS = "game_focus",
}

OtherEventType = {
	VIEW_OPEN = "view_open",		-- 界面打开
	VIEW_CLOSE = "view_close",		-- 界面关闭
}

GameNetEvent = {
	OnDisconnect = "ondisconnect", 	-- 异常断线
	OnSendError = "onsenderror",	-- 数据发送异常
	OnReConnect = "onreconnect",	-- 断线重连
}