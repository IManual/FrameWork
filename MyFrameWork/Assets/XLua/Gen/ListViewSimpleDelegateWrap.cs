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
    public class ListViewSimpleDelegateWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(ListViewSimpleDelegate);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 4, 4);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetNumberOfCells", _m_GetNumberOfCells);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCellViewSize", _m_GetCellViewSize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCellView", _m_GetCellView);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateCell", _m_CreateCell);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "CellPrefab", _g_get_CellPrefab);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NumberOfCellsDel", _g_get_NumberOfCellsDel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CellSizeDel", _g_get_CellSizeDel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CellRefreshDel", _g_get_CellRefreshDel);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "CellPrefab", _s_set_CellPrefab);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NumberOfCellsDel", _s_set_NumberOfCellsDel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CellSizeDel", _s_set_CellSizeDel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CellRefreshDel", _s_set_CellRefreshDel);
            
			
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
					
					ListViewSimpleDelegate gen_ret = new ListViewSimpleDelegate();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to ListViewSimpleDelegate constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNumberOfCells(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ListViewSimpleDelegate gen_to_be_invoked = (ListViewSimpleDelegate)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    EnhancedUI.EnhancedScroller.EnhancedScroller _scroller = (EnhancedUI.EnhancedScroller.EnhancedScroller)translator.GetObject(L, 2, typeof(EnhancedUI.EnhancedScroller.EnhancedScroller));
                    
                        int gen_ret = gen_to_be_invoked.GetNumberOfCells( _scroller );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCellViewSize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ListViewSimpleDelegate gen_to_be_invoked = (ListViewSimpleDelegate)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    EnhancedUI.EnhancedScroller.EnhancedScroller _scroller = (EnhancedUI.EnhancedScroller.EnhancedScroller)translator.GetObject(L, 2, typeof(EnhancedUI.EnhancedScroller.EnhancedScroller));
                    int _dataIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                        float gen_ret = gen_to_be_invoked.GetCellViewSize( _scroller, _dataIndex );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCellView(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ListViewSimpleDelegate gen_to_be_invoked = (ListViewSimpleDelegate)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    EnhancedUI.EnhancedScroller.EnhancedScroller _scroller = (EnhancedUI.EnhancedScroller.EnhancedScroller)translator.GetObject(L, 2, typeof(EnhancedUI.EnhancedScroller.EnhancedScroller));
                    int _dataIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _cellIndex = LuaAPI.xlua_tointeger(L, 4);
                    
                        EnhancedUI.EnhancedScroller.EnhancedScrollerCellView gen_ret = gen_to_be_invoked.GetCellView( _scroller, _dataIndex, _cellIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateCell(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ListViewSimpleDelegate gen_to_be_invoked = (ListViewSimpleDelegate)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        ListViewCell gen_ret = gen_to_be_invoked.CreateCell(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CellPrefab(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ListViewSimpleDelegate gen_to_be_invoked = (ListViewSimpleDelegate)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.CellPrefab);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NumberOfCellsDel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ListViewSimpleDelegate gen_to_be_invoked = (ListViewSimpleDelegate)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NumberOfCellsDel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CellSizeDel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ListViewSimpleDelegate gen_to_be_invoked = (ListViewSimpleDelegate)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.CellSizeDel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CellRefreshDel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ListViewSimpleDelegate gen_to_be_invoked = (ListViewSimpleDelegate)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.CellRefreshDel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CellPrefab(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ListViewSimpleDelegate gen_to_be_invoked = (ListViewSimpleDelegate)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CellPrefab = (ListViewCell)translator.GetObject(L, 2, typeof(ListViewCell));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NumberOfCellsDel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ListViewSimpleDelegate gen_to_be_invoked = (ListViewSimpleDelegate)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NumberOfCellsDel = translator.GetDelegate<ListViewSimpleDelegate.NumberOfCellsDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CellSizeDel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ListViewSimpleDelegate gen_to_be_invoked = (ListViewSimpleDelegate)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CellSizeDel = translator.GetDelegate<ListViewSimpleDelegate.CellSizeDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CellRefreshDel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ListViewSimpleDelegate gen_to_be_invoked = (ListViewSimpleDelegate)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CellRefreshDel = translator.GetDelegate<ListViewSimpleDelegate.CellRefreshDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
