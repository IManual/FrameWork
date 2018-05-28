MainUIView = MainUIView or BaseClass(BaseView)

function MainUIView:__init()
	self.ui_config = {"uis/views/mainui","MainView"}
end

function MainUIView:__delete()
	
end

function MainUIView:LoadCallBack()
	print("LoadCallBack")
end

-- 销毁前调用
function MainUIView:ReleaseCallBack()
	print("ReleaseCallBack")
end

function MainUIView:OpenCallBack()
	print("OpenCallBack")
end

-- 关闭前调用
function MainUIView:CloseCallBack()
	print("CloseCallBack")
end

-- 切换标签调用
function MainUIView:ShowIndexCallBack(index)
	-- body
end

function MainUIView:OnFlush(param_list)
	-- body
end
