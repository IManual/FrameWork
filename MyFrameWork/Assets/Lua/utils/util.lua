function IsNil(uobj)
	return uobj == nil or uobj:Equals(nil)
end

function Split(split_string, splitter)
	-- 以某个分隔符为标准，分割字符串
	-- @param split_string 需要分割的字符串
	-- @param splitter 分隔符
	-- @return 用分隔符分隔好的table

	local split_result = {}
	local search_pos_begin = 1

	while true do
		local find_pos_begin, find_pos_end = string.find(split_string, splitter, search_pos_begin)
		if not find_pos_begin then
			break
		end

		split_result[#split_result + 1] = string.sub(split_string, search_pos_begin, find_pos_begin - 1)
		search_pos_begin = find_pos_end + 1
	end

	if search_pos_begin <= string.len(split_string) then
		split_result[#split_result + 1] = string.sub(split_string, search_pos_begin)
	end

	return split_result
end

function print_log(msg)
	print(msg)
end

function print_error(msg)	
	local str = ""
	local info = ""
	str = debug.traceback()
	info = Split(str,"\n")[3]
	info = Split(info,":")[1]..":"..Split(info,":")[2]
	info =  "["..string.gsub(info, "\t", "@").."]:";
	if (type(msg)=="table") then
		str = info..tabletostring(msg).."\n"..str
	else
		str = info..msg.."\n"..str
	end
	LogForLua.PrintError(str)
end