
-----------------------------------------------------------------------------
-- JSON4Lua: JSON encoding / decoding support for the Lua language.
-- json Module.
-- Author: Craig Mason-Jones
-- Homepage: http://json.luaforge.net/
-- Version: 0.9.40
-- This module is released under the MIT License (MIT).
-- Please see LICENCE.txt for details.
--
-- USAGE:
-- This module exposes two functions:
--   encode(o)
--     Returns the table / string / boolean / number / nil / json.null value as a JSON-encoded string.
--   decode(json_string)
--     Returns a Lua object populated with the data encoded in the JSON string json_string.
--
-- REQUIREMENTS:
--   compat-5.1 if using Lua 5.0
--
-- CHANGELOG
--   0.9.20 Introduction of local Lua functions for private functions (removed _ function prefix). 
--          Fixed Lua 5.1 compatibility issues.
--   		Introduced json.null to have null values in associative arrays.
--          encode() performance improvement (more than 50%) through table.concat rather than ..
--          Introduced decode ability to ignore /**/ comments in the JSON string.
--   0.9.10 Fix to array encoding / decoding to correctly manage nil/null values in arrays.
-----------------------------------------------------------------------------

-----------------------------------------------------------------------------
-- Imports and dependencies
-----------------------------------------------------------------------------
local math = require('math')
local string = require("string")
local table = require("table")

local base = _G

-----------------------------------------------------------------------------

-- Public functions

-- Private functions
local encodeString
local isArray
local isEncodable

-----------------------------------------------------------------------------
-- PUBLIC FUNCTIONS
-----------------------------------------------------------------------------
--- Encodes an arbitrary Lua object / variable.
-- @param v The Lua object / variable to be JSON encoded.
-- @return String containing the JSON encoding in internal Lua string format (i.e. not unicode)
function tabletostring(v)
  -- Handle nil values
  if v==nil then
    return "null"
  end
  
  local vtype = base.type(v)  

  -- Handle strings
  if vtype=='string' then    
    return '"' .. encodeString(v) .. '"'	    -- Need to handle encoding in string
  end
  
  -- Handle booleans
  if vtype=='number' or vtype=='boolean' then
    return base.tostring(v)
  end
  
  -- Handle tables
  if vtype=='table' then
    local rval = {}
    -- Consider arrays separately
    local bArray, maxCount = isArray(v)
    if bArray then
      for i = 1,maxCount do
        table.insert(rval, encode(v[i]))
      end
    else	-- An object, not an array
      for i,j in base.pairs(v) do
        if isEncodable(i) and isEncodable(j) then
          table.insert(rval, '"' .. encodeString(i) .. '"=' .. encode(j))
        end
      end
    end
    return '{' .. table.concat(rval,',') ..'}'
  end
  
  -- Handle null values
  if vtype=='function' and v==null then
    return 'null'
  end
  
  base.assert(false,'encode attempt to encode unsupported type ' .. vtype .. '=' .. base.tostring(v))
end

--- Encodes a string to be JSON-compatible.
-- This just involves back-quoting inverted commas, back-quotes and newlines, I think ;-)
-- @param s The string to return as a JSON encoded (i.e. backquoted string)
-- @return The string appropriately escaped.

local escapeList = {
    ['"']  = '\\"',
    ['\\'] = '\\\\',
    ['/']  = '\\/', 
    ['\b'] = '\\b',
    ['\f'] = '\\f',
    ['\n'] = '\\n',
    ['\r'] = '\\r',
    ['\t'] = '\\t'
}

function encodeString(s)
 return s:gsub(".", function(c) return escapeList[c] end) -- SoniEx2: 5.0 compat
end

function isArray(t)
  -- Next we count all the elements, ensuring that any non-indexed elements are not-encodable 
  -- (with the possible exception of 'n')
  local maxIndex = 0
  for k,v in base.pairs(t) do
    if (base.type(k)=='number' and math.floor(k)==k and 1<=k) then	-- k,v is an indexed pair
      if (not isEncodable(v)) then return false end	-- All array elements must be encodable
      maxIndex = math.max(maxIndex,k)
    else
      if (k=='n') then
        if v ~= table.getn(t) then return false end  -- False if n does not hold the number of elements
      else -- Else of (k=='n')
        if isEncodable(v) then return false end
      end  -- End of (k~='n')
    end -- End of k,v not an indexed pair
  end  -- End of loop across all pairs
  return true, maxIndex
end

--- Determines whether the given Lua object / table / variable can be JSON encoded. The only
-- types that are JSON encodable are: string, boolean, number, nil, table and json.null.
-- In this implementation, all other types are ignored.
-- @param o The object to examine.
-- @return boolean True if the object should be JSON encoded, false if it should be ignored.
function isEncodable(o)
  local t = base.type(o)
  return (t=='string' or t=='boolean' or t=='number' or t=='nil' or t=='table') or (t=='function' and o==null) 
end


