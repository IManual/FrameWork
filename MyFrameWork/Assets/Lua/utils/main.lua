GameObject = CS.UnityEngine.GameObject
MainCamera = CS.UnityEngine.Camera.main
Resources = CS.UnityEngine.Resources
Vector2 = CS.UnityEngine.Vector2
Vector3 = CS.UnityEngine.Vector3
Color = CS.UnityEngine.Color
GameNet = CS.GameNet
Request = CS.Request

GlobalEventSystem = CS.GlobalEventSystem				-- 全局事件系统
TimeControl = CS.TimeControl							-- 计时器
GlobalTimeRequest = CS.GlobalTimeRequest      			-- 倒计时工具
AssetLoaderManager = CS.AssetLoaderManager				-- 资源加载工具
LogForLua = CS.LogForLua								-- Log工具

function GameStart()
	print("GameStart")
	ReqireLua()
	InitModule()
	ViewManager.Instance:Open(ViewName.MainUI)
end

function ReqireLua()
	local list = require("game/common/require_list")
	for _, path in ipairs(list) do
		--if string.match(path, "^config/auto_new/.*") then
			--CheckLuaConfig(path, require(path))
		--else
			require(path)
		--end
	end
end

local module_list = {}
function InitModule()
	module_list = {}
	table.insert(module_list, ModulesController.New())
	table.insert(module_list, LoadingPriorityManager.New())
end

local updat_request_list = {}
function Update()
	local time = CS.UnityEngine.Time.unscaledTime
	local delta_time = CS.UnityEngine.Time.unscaledDeltaTime
	--time_request:Update()
	GlobalTimeRequest.Update()

	for k, v in pairs(updat_request_list) do
		v(time, delta_time)
	end
end

function AddToUpdate(func)
	if type(func) == "function" then
		table.insert(updat_request_list, func)
	else
		print("func is not a type of function")
	end
end

function Stop()
	for i=#module_list, 1, -1 do
		module_list[i]:DeleteMe()
	end
	module_list = {}
	updat_request_list = {}

	GlobalEventSystem.UnBindAll()

	print("Stop")
end

function Focus()
	print("Focus")
end

function Pause()
	print("Pause")
end

function ExecuteGm(gm)
	print("ExecuteGm")
end

function Collectgarbage(param)
	print("Collectgarbage")
end

GameStart()