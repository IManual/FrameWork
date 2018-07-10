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
    public class UIEventTableWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UIEventTable);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ListenEvent", _m_ListenEvent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEventHandler", _m_GetEventHandler);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearEvent", _m_ClearEvent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearAllEvent", _m_ClearAllEvent);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "events", _g_get_events);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "events", _s_set_events);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					UIEventTable gen_ret = new UIEventTable();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UIEventTable constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ListenEvent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UIEventTable gen_to_be_invoked = (UIEventTable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.Events.UnityAction _call = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 3);
                    
                    gen_to_be_invoked.ListenEvent( _eventName, _call );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEventHandler(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UIEventTable gen_to_be_invoked = (UIEventTable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.Events.UnityAction gen_ret = gen_to_be_invoked.GetEventHandler( _eventName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearEvent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UIEventTable gen_to_be_invoked = (UIEventTable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.ClearEvent( _eventName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearAllEvent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UIEventTable gen_to_be_invoked = (UIEventTable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ClearAllEvent(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_events(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIEventTable gen_to_be_invoked = (UIEventTable)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.events);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_events(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIEventTable gen_to_be_invoked = (UIEventTable)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.events = (UIEvent[])translator.GetObject(L, 2, typeof(UIEvent[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
