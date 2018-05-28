BindTool = BindTool or {}

function BindTool.UnPack(param, count, i, ...)
	if i >= count then
		if i == count then
			return param[i], ...
		end
		return ...
	end
	return param[i], BindTool.UnPack(param, count, i + 1, ...)
end

-- 绑定带有参数的函数 作为一个新的参数返回
function BindTool.Bind(func, ...)
	if type(func) ~= "function" then
		print_error("BindTool.Bind error!")
		return function() end
	end

	local count = select('#', ...)
	local param = {...}
	local new_func = nil

	if 0 == count then
		new_func = function(...) return func(...) end
	elseif 1 == count then
		new_func = function(...) return func(param[1], ...) end
	elseif 2 == count then
		new_func = function(...) return func(param[1], param[2], ...) end
	else
		new_func = function(...) return func(BindTool.UnPack(param, count, 1, ...)) end
	end

	return new_func
end