MainUIData = MainUIData or BaseClass()

function MainUIData:__init()
	if MainUIData.Instance then
		print_error("[MainUIData] Attempt to create singleton twice!")
		return
	end
	MainUIData.Instance = self
end

function MainUIData:__delete()
	MainUIData.Instance = nil
end
