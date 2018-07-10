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
    public class GameNetWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(GameNet);
			Utils.BeginObjectRegister(type, L, translator, 0, 14, 4, 4);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DebugReturn", _m_DebugReturn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnEvent", _m_OnEvent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnOperationResponse", _m_OnOperationResponse);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnStatusChanged", _m_OnStatusChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetOnSendErrorCallBack", _m_SetOnSendErrorCallBack);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetOnDisconnectCallBack", _m_SetOnDisconnectCallBack);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetOnReConnectCallBack", _m_SetOnReConnectCallBack);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReConnect", _m_ReConnect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterProtocol", _m_RegisterProtocol);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterProtocolHandler", _m_RegisterProtocolHandler);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DisConnected", _m_DisConnected);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Release", _m_Release);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnRegisterProtocol", _m_UnRegisterProtocol);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_socket", _g_get_m_socket);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnDisconnect", _g_get_OnDisconnect);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnSendError", _g_get_OnSendError);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnReConnect", _g_get_OnReConnect);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_socket", _s_set_m_socket);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnDisconnect", _s_set_OnDisconnect);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnSendError", _s_set_OnSendError);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnReConnect", _s_set_OnReConnect);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 0);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Instance", _g_get_Instance);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					GameNet gen_ret = new GameNet();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to GameNet constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DebugReturn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ExitGames.Client.Photon.DebugLevel _level;translator.Get(L, 2, out _level);
                    string _message = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.DebugReturn( _level, _message );
                    
                    
                    
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
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_OnOperationResponse(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Update(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnStatusChanged(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ExitGames.Client.Photon.StatusCode _statusCode;translator.Get(L, 2, out _statusCode);
                    
                    gen_to_be_invoked.OnStatusChanged( _statusCode );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetOnSendErrorCallBack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Events.UnityAction _call = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
                    
                    gen_to_be_invoked.SetOnSendErrorCallBack( _call );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetOnDisconnectCallBack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Events.UnityAction _call = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
                    
                    gen_to_be_invoked.SetOnDisconnectCallBack( _call );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetOnReConnectCallBack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Events.UnityAction _call = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
                    
                    gen_to_be_invoked.SetOnReConnectCallBack( _call );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReConnect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ReConnect(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterProtocol(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ProtocolBase _protocol = (ProtocolBase)translator.GetObject(L, 2, typeof(ProtocolBase));
                    
                    gen_to_be_invoked.RegisterProtocol( _protocol );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterProtocolHandler(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ProtocolBase _protocol = (ProtocolBase)translator.GetObject(L, 2, typeof(ProtocolBase));
                    ProtocolHandler _handeler = translator.GetDelegate<ProtocolHandler>(L, 3);
                    
                    gen_to_be_invoked.RegisterProtocolHandler( _protocol, _handeler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DisConnected(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.DisConnected(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Release(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Release(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnRegisterProtocol(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte _opCode = (byte)LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.UnRegisterProtocol( _opCode );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Instance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, GameNet.Instance);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_socket(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.m_socket);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnDisconnect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnDisconnect);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnSendError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnSendError);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnReConnect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnReConnect);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_socket(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.m_socket = (System.Net.Sockets.Socket)translator.GetObject(L, 2, typeof(System.Net.Sockets.Socket));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnDisconnect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnDisconnect = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnSendError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnSendError = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnReConnect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameNet gen_to_be_invoked = (GameNet)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnReConnect = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
