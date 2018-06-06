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
    public class ProtocolBaseWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(ProtocolBase);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 2, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnOperationResponse", _m_OnOperationResponse);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnEvent", _m_OnEvent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DefaltRequest", _m_DefaltRequest);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "opCode", _g_get_opCode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "handler", _g_get_handler);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "opCode", _s_set_opCode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "handler", _s_set_handler);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "ProtocolBase does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnOperationResponse(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ProtocolBase gen_to_be_invoked = (ProtocolBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ExitGames.Client.Photon.OperationResponse _operationResponse = (ExitGames.Client.Photon.OperationResponse)translator.GetObject(L, 2, typeof(ExitGames.Client.Photon.OperationResponse));
                    
                    gen_to_be_invoked.OnOperationResponse( _operationResponse );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnEvent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ProtocolBase gen_to_be_invoked = (ProtocolBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ExitGames.Client.Photon.EventData _eventData = (ExitGames.Client.Photon.EventData)translator.GetObject(L, 2, typeof(ExitGames.Client.Photon.EventData));
                    
                    gen_to_be_invoked.OnEvent( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DefaltRequest(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ProtocolBase gen_to_be_invoked = (ProtocolBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.DefaltRequest(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_opCode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ProtocolBase gen_to_be_invoked = (ProtocolBase)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.opCode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_handler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ProtocolBase gen_to_be_invoked = (ProtocolBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.handler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_opCode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ProtocolBase gen_to_be_invoked = (ProtocolBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.opCode = (byte)LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_handler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ProtocolBase gen_to_be_invoked = (ProtocolBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.handler = translator.GetDelegate<ProtocolHandler>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
