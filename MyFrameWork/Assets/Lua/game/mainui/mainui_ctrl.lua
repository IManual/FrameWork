require("game/mainui/mainui_data")
require("game/mainui/mainui_view")

MainUICtrl = MainUICtrl or BaseClass(BaseController)
function MainUICtrl:__init()
	if MainUICtrl.Instance ~= nil then
		print_error("[MainUICtrl] attempt to create singleton twice!")
		return
	end
	MainUICtrl.Instance = self
	self.view = MainUIView.New(ViewName.MainUI)
	self.data = MainUIData.New()
end

function MainUICtrl:__delete()
	MainUICtrl.Instance = nil

	if self.data then
		self.data:DeleteMe()
		self.data = nil
	end

	if self.view then
		self.view:DeleteMe()
		self.view = nil
	end
end