--保存类型的虚表
local _class = {}

function BaseClass(super)
	--生成一个类类型
	local class_type = {}
	--在创建对象的时候自动调用
	class_type.__init = false
	class_type.__delete = false
	class_type.super = super
	-- 提供New方法 创建脚本对象
	class_type.New = function(...)
		-- 生成一个类对象
		local obj = {_class_type = class_type}

		-- 在初始化之前注册基类方法
		setmetatable(obj, { __index = _class[class_type] })

		-- 递归调用所有基类初始化方法  类似构造函数
		do 
			local create
			create = function(c, ...)
				-- 递归查找基类
				if c.super then
					create(c.super, ...)
				end
				-- 如果当前类有__init方法  调用 
				if c.__init then
					c.__init(obj, ...)
				end
			end
			create(class_type, ...)
		end

		-- 注册一个DeleteMe方法 		类似析构函数
		obj.DeleteMe = function(self)
			local now_super = self._class_type
			while now_super ~= nil do
				if now_super.__delete then
					now_super.__delete(self)
				end
				now_super = now_super.super
			end
		end

		return obj
	end

	local vtbl = {}
	_class[class_type] = vtbl

	setmetatable(class_type, {__newindex = function(t,k,v) vtbl[k] = v end,
		__index = vtbl, --For call parent method
	})

	if super then
		setmetatable(vtbl, {__index = function(t,k) return _class[super][k] end})
	end

	return class_type
end