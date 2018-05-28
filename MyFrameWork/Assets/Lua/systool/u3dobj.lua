local component_table = {
	-- 基础组件
	transform = CS.UnityEngine.Transform,
	camera = CS.UnityEngine.Camera,
	renderer = CS.UnityEngine.Renderer,
	animation = CS.UnityEngine.Animation,
	animator = CS.UnityEngine.Animator,
	collider = CS.UnityEngine.Collider,
	audio = CS.UnityEngine.Audio,
	light = CS.UnityEngine.Light,
	line_renderer = CS.UnityEngine.LineRenderer,

	-- UI相关组件
	rect = CS.UnityEngine.RectTransform,
	canvas = CS.UnityEngine.Canvas,
	canvas_group = CS.UnityEngine.CanvasGroup,
	image = CS.UnityEngine.UI.Image,
	raw_image = CS.UnityEngine.UI.RawImage,
	text = CS.UnityEngine.UI.Text,
	button = CS.UnityEngine.UI.Button,
	toggle = CS.UnityEngine.UI.Toggle,
	toggle_group = CS.UnityEngine.UI.ToggleGroup,
	slider = CS.UnityEngine.UI.Slider,
	scroll_rect = CS.UnityEngine.UI.ScrollRect,
	input_field = CS.UnityEngine.UI.InputField,
	dropdown = CS.UnityEngine.UI.Dropdown,
	outline = CS.UnityEngine.UI.Outline,
	shadow = CS.UnityEngine.UI.Shadow,	
	grid_layout_group = CS.UnityEngine.UI.GridLayoutGroup,
}

local u3d_shortcut = {}
function u3d_shortcut:SetActive(active)
	self.gameObject:SetActive(active)
end

function u3d_shortcut:GetActive()
	return self.gameObject.activeInHierarchy
end

function u3d_shortcut:FindObj(name_path)
	local transform = self.transform:FindHard(name_path)
	if transform == nil then
		return nil
	end

	return U3DObject(transform.gameObject, transform)
end

function u3d_shortcut:GetComponent(type)
	return self.gameObject:GetComponent(type)
end

function u3d_shortcut:GetOrAddComponent(type)
	return self.gameObject:GetOrAddComponent(type)
end

function u3d_shortcut:GetComponentsInChildren(type)
	return self.gameObject:GetComponentsInChildren(type)
end

function u3d_shortcut:SetLocalPosition(x, y, z)
	self.transform:SetLocalPosition(x or 0, y or 0, z or 0)
end

function u3d_shortcut:SetLocalScale(x, y, z)
	self.transform.localScale = Vector3(x or 0, y or 0, z or 0)
end

local u3d_metatable = {
	-- 通过键访问时 将自身table作为第一个参数传递
	__index = function(table, key)
		if IsNil(table.gameObject) then
			return nil
		end
		-- 查找组件表中注册的组件
		local key_type = component_table[key]
		if key_type ~= nil then
			local component = table.gameObject:GetComponent(key_type)
			if component ~= nil then
				table[key] = component
				return component
			end
		end
		-- 如果没有
		return u3d_shortcut[key]
	end
}

function U3DObject(go, transform)
	if go == nil then
		return nil
	end

	local obj = {gameObject = go, transform = transform, }
	setmetatable(obj, u3d_metatable)
	return obj
end