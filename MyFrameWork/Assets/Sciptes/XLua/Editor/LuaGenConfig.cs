using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

public static class LuaGenConfig
{
    [LuaCallCSharp]
    public static List<Type> my_module_lua_call_cs_list = new List<Type>()
    {
        typeof(GameObject),
        typeof(RectTransform),
        typeof(Canvas),
        typeof(CanvasGroup),
        typeof(Image),
        typeof(RawImage),
        typeof(Text),
        typeof(Button),
        typeof(Toggle),
        typeof(ToggleGroup),
        typeof(Slider),
        typeof(ScrollRect),
        typeof(InputField),
        typeof(Dropdown),
        typeof(Outline),
        typeof(Shadow),

        typeof(GlobalTimeRequest),
        typeof(GlobalEventSystem),
        typeof(TimeControl),
        typeof(GameNet),
        typeof(ProtocolBase),

        typeof(UIEventTable),
        typeof(UINameTable),
        typeof(UIVariableTable),

        typeof(TransformExtend),
        typeof(ToggleExtend),
        typeof(ButtonExtend),
        typeof(GameObjectExtend),

        typeof(ListViewCell),
        typeof(ListViewSimpleDelegate),
        typeof(UIPrefabLoader),

        typeof(AssetLoaderManager),
    };

    [CSharpCallLua]
    public static List<Type> my_module_cs_call_lua_list = new List<Type>()
    {
        typeof(Action),
        typeof(Action<GameObject>),
        typeof(System.Collections.IEnumerator),
        typeof(Action<UIVariable>),

        typeof(ListViewSimpleDelegate.NumberOfCellsDelegate),
        typeof(ListViewSimpleDelegate.CellRefreshDelegate),
        typeof(ListViewSimpleDelegate.CellSizeDelegate),
    };
}