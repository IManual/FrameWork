local component_table = {
	-- 基础组件
	transform = typeof(CS.UnityEngine.Transform),
	camera = typeof(CS.UnityEngine.Camera),
	renderer = typeof(CS.UnityEngine.Renderer),
	animation = typeof(CS.UnityEngine.Animation),
	animator = typeof(CS.UnityEngine.Animator),
	collider = typeof(CS.UnityEngine.Collider),
	audio = typeof(CS.UnityEngine.Audio),
	light = typeof(CS.UnityEngine.Light),
	line_renderer = typeof(CS.UnityEngine.LineRenderer),

	-- UI相关组件
	rect = typeof(CS.UnityEngine.RectTransform),
	canvas = typeof(CS.UnityEngine.Canvas),
	canvas_group = typeof(CS.UnityEngine.CanvasGroup),
	image = typeof(CS.UnityEngine.UI.Image),
	raw_image = typeof(CS.UnityEngine.UI.RawImage),
	text = typeof(CS.UnityEngine.UI.Text),
	button = typeof(CS.UnityEngine.UI.Button),
	toggle = typeof(CS.UnityEngine.UI.Toggle),
	toggle_group = typeof(CS.UnityEngine.UI.ToggleGroup),
	slider = typeof(CS.UnityEngine.UI.Slider),
	scroll_rect = typeof(CS.UnityEngine.UI.ScrollRect),
	input_field = typeof(CS.UnityEngine.UI.InputField),
	dropdown = typeof(CS.UnityEngine.UI.Dropdown),
	outline = typeof(CS.UnityEngine.UI.Outline),
	shadow = typeof(CS.UnityEngine.UI.Shadow),	
	grid_layout_group = typeof(CS.UnityEngine.UI.GridLayoutGroup),

	-- 自定义UI组件
	list_cell = typeof(CS.ListViewCell),
	list_simple_delegate = typeof(CS.ListViewSimpleDelegate),
	uiprefab_loader = typeof(CS.UIPrefabLoader),
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