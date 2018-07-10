#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class GlobalTimeRequestWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(GlobalTimeRequest);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 4, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Update", _m_Update_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddDelayTime", _m_AddDelayTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CancleTime", _m_CancleTime_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "GlobalTimeRequest does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    GlobalTimeRequest.Update(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddDelayTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<UnityEngine.Events.UnityAction>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    float _time = (float)LuaAPI.lua_tonumber(L, 1);
                    UnityEngine.Events.UnityAction _callBack = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
                    bool _isRepeat = LuaAPI.lua_toboolean(L, 3);
                    
                        TimeEvent gen_ret = GlobalTimeRequest.AddDelayTime( _time, _callBack, _isRepeat );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<UnityEngine.Events.UnityAction>(L, 2)) 
                {
                    float _time = (float)LuaAPI.lua_tonumber(L, 1);
                    UnityEngine.Events.UnityAction _callBack = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
                    
                        TimeEvent gen_ret = GlobalTimeRequest.AddDelayTime( _time, _callBack );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to GlobalTimeRequest.AddDelayTime!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CancleTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TimeEvent _timer = (TimeEvent)translator.GetObject(L, 1, typeof(TimeEvent));
                    
                    GlobalTimeRequest.CancleTime( _timer );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
