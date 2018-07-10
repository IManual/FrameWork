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
    public class TimeControlWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TimeControl);
			Utils.BeginObjectRegister(type, L, translator, 0, 6, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLeftTime", _m_GetLeftTime);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DestoryTimer", _m_DestoryTimer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PauseTimer", _m_PauseTimer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReStartTimer", _m_ReStartTimer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeTargetTime", _m_ChangeTargetTime);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartTiming", _m_StartTiming);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "CreaterTimer", _m_CreaterTimer_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					TimeControl gen_ret = new TimeControl();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to TimeControl constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLeftTime(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TimeControl gen_to_be_invoked = (TimeControl)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        float gen_ret = gen_to_be_invoked.GetLeftTime(  );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestoryTimer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TimeControl gen_to_be_invoked = (TimeControl)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.DestoryTimer(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PauseTimer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TimeControl gen_to_be_invoked = (TimeControl)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.PauseTimer(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReStartTimer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TimeControl gen_to_be_invoked = (TimeControl)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ReStartTimer(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeTargetTime(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TimeControl gen_to_be_invoked = (TimeControl)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _time_ = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.ChangeTargetTime( _time_ );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartTiming(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TimeControl gen_to_be_invoked = (TimeControl)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 8&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<CompleteEvent>(L, 3)&& translator.Assignable<UpdateEvent>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 8)) 
                {
                    float _time_ = (float)LuaAPI.lua_tonumber(L, 2);
                    CompleteEvent _onCompleted_ = translator.GetDelegate<CompleteEvent>(L, 3);
                    UpdateEvent _updateEvent_ = translator.GetDelegate<UpdateEvent>(L, 4);
                    float _repateRate_ = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _isIgnoreTimeScale_ = LuaAPI.lua_toboolean(L, 6);
                    bool _isRepeate_ = LuaAPI.lua_toboolean(L, 7);
                    bool _isDestory_ = LuaAPI.lua_toboolean(L, 8);
                    
                    gen_to_be_invoked.StartTiming( _time_, _onCompleted_, _updateEvent_, _repateRate_, _isIgnoreTimeScale_, _isRepeate_, _isDestory_ );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<CompleteEvent>(L, 3)&& translator.Assignable<UpdateEvent>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    float _time_ = (float)LuaAPI.lua_tonumber(L, 2);
                    CompleteEvent _onCompleted_ = translator.GetDelegate<CompleteEvent>(L, 3);
                    UpdateEvent _updateEvent_ = translator.GetDelegate<UpdateEvent>(L, 4);
                    float _repateRate_ = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _isIgnoreTimeScale_ = LuaAPI.lua_toboolean(L, 6);
                    bool _isRepeate_ = LuaAPI.lua_toboolean(L, 7);
                    
                    gen_to_be_invoked.StartTiming( _time_, _onCompleted_, _updateEvent_, _repateRate_, _isIgnoreTimeScale_, _isRepeate_ );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<CompleteEvent>(L, 3)&& translator.Assignable<UpdateEvent>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    float _time_ = (float)LuaAPI.lua_tonumber(L, 2);
                    CompleteEvent _onCompleted_ = translator.GetDelegate<CompleteEvent>(L, 3);
                    UpdateEvent _updateEvent_ = translator.GetDelegate<UpdateEvent>(L, 4);
                    float _repateRate_ = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _isIgnoreTimeScale_ = LuaAPI.lua_toboolean(L, 6);
                    
                    gen_to_be_invoked.StartTiming( _time_, _onCompleted_, _updateEvent_, _repateRate_, _isIgnoreTimeScale_ );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<CompleteEvent>(L, 3)&& translator.Assignable<UpdateEvent>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _time_ = (float)LuaAPI.lua_tonumber(L, 2);
                    CompleteEvent _onCompleted_ = translator.GetDelegate<CompleteEvent>(L, 3);
                    UpdateEvent _updateEvent_ = translator.GetDelegate<UpdateEvent>(L, 4);
                    float _repateRate_ = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.StartTiming( _time_, _onCompleted_, _updateEvent_, _repateRate_ );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<CompleteEvent>(L, 3)&& translator.Assignable<UpdateEvent>(L, 4)) 
                {
                    float _time_ = (float)LuaAPI.lua_tonumber(L, 2);
                    CompleteEvent _onCompleted_ = translator.GetDelegate<CompleteEvent>(L, 3);
                    UpdateEvent _updateEvent_ = translator.GetDelegate<UpdateEvent>(L, 4);
                    
                    gen_to_be_invoked.StartTiming( _time_, _onCompleted_, _updateEvent_ );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<CompleteEvent>(L, 3)) 
                {
                    float _time_ = (float)LuaAPI.lua_tonumber(L, 2);
                    CompleteEvent _onCompleted_ = translator.GetDelegate<CompleteEvent>(L, 3);
                    
                    gen_to_be_invoked.StartTiming( _time_, _onCompleted_ );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TimeControl.StartTiming!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreaterTimer_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _gobjName = LuaAPI.lua_tostring(L, 1);
                    
                        TimeControl gen_ret = TimeControl.CreaterTimer( _gobjName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 0) 
                {
                    
                        TimeControl gen_ret = TimeControl.CreaterTimer(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TimeControl.CreaterTimer!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
