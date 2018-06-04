
-- 需要require的文件列表
local lua_file_list ={
	"systool/systool",

	"utils/util",

	"gamenet/msgadapter",
	"gamenet/net_manager",
	"protocolcommon/userprotocol/user_protocol",

	"config/config_manager",
	"manager/loading_priority_manager",

	"game/common/modules_controller",
	"game/common/global_events",
	"game/common/game_enum",
	"game/common/base_controller",
	"game/common/base_view",
	"game/common/base_render",

	--需要放在ctrl 类前，common 后
	"language/language",

	"gameui/game_ui",
	"game/mainui/mainui_ctrl",
}

return lua_file_list