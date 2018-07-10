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
    public class UIVariableTableWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UIVariableTable);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 2, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FlushVariableDic", _m_FlushVariableDic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRepeatVariable", _m_GetRepeatVariable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FindVariable", _m_FindVariable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddDefaultVariable", _m_AddDefaultVariable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetVariableNames", _m_GetVariableNames);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitializeBinds", _m_InitializeBinds);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetVariable", _m_GetVariable);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Variables", _g_get_Variables);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "variableDic", _g_get_variableDic);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "variableDic", _s_set_variableDic);
            
			
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
					
					UIVariableTable gen_ret = new UIVariableTable();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UIVariableTable constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FlushVariableDic(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UIVariableTable gen_to_be_invoked = (UIVariableTable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.FlushVariableDic(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRepeatVariable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UIVariableTable gen_to_be_invoked = (UIVariableTable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.Generic.List<string> gen_ret = gen_to_be_invoked.GetRepeatVariable(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindVariable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UIVariableTable gen_to_be_invoked = (UIVariableTable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        UIVariable gen_ret = gen_to_be_invoked.FindVariable( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddDefaultVariable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UIVariableTable gen_to_be_invoked = (UIVariableTable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.AddDefaultVariable(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetVariableNames(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UIVariableTable gen_to_be_invoked = (UIVariableTable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string[] gen_ret = gen_to_be_invoked.GetVariableNames(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitializeBinds(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UIVariableTable gen_to_be_invoked = (UIVariableTable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.InitializeBinds(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetVariable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UIVariableTable gen_to_be_invoked = (UIVariableTable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        UIVariable gen_ret = gen_to_be_invoked.GetVariable( _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Variables(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIVariableTable gen_to_be_invoked = (UIVariableTable)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Variables);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_variableDic(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIVariableTable gen_to_be_invoked = (UIVariableTable)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.variableDic);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_variableDic(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIVariableTable gen_to_be_invoked = (UIVariableTable)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.variableDic = (System.Collections.Generic.Dictionary<string, UIVariable>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, UIVariable>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
