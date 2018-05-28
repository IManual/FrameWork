function IsNil(uobj)
	return uobj == nil or uobj:Equals(nil)
end

function print_log(msg)
	print(msg)
end

function print_error(msg)
	error(msg)
end