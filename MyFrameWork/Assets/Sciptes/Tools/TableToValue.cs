using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI变量改变时触发对应事件的工具类 UIVarChangeEventTool
/// </summary>
public class TableToValue {
    
    /// <summary>
    /// 变量名字和此变量改变时要回调的方法的字典
    /// </summary>
    static Dictionary<string, Action<UIVariable>> eventAction;

    static TableToValue()
    {
        eventAction = new Dictionary<string, Action<UIVariable>>();
    }

    /// <summary>
    /// 触发变量改变的事件
    /// </summary>
    public static void FireEvent(string key,UIVariable variable)
    {
        if (eventAction.ContainsKey(key))
        {
            eventAction[key](variable);
        }
    }

    /// <summary>
    /// 监听事件
    /// </summary>

    public static void RegistEvent(Action<UIVariable> action,string key)
    {
        if (eventAction.ContainsKey(key))
        {
            eventAction[key] += action;
            return;
        }
        eventAction.Add(key, action);
    }

    /// <summary>
    /// 取消对事件的监听
    /// </summary>
    public static void UnRegistEvent(string key)
    {
        if (eventAction.ContainsKey(key))
        {
            eventAction.Remove(key);
        }
    }
}
