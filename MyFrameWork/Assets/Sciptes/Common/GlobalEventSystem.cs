using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 全局事件系统
/// </summary>
public static class GlobalEventSystem {

    /// <summary>
    /// 全局事件与对应处理方法的字典
    /// </summary>
    private static Dictionary<string, UnityAction> actionTree = new Dictionary<string, UnityAction>();

    /// <summary>
    /// 绑定全局事件和对应的处理方法
    /// </summary>
    public static void Bind(string type, UnityAction call)
    {
        if (actionTree.ContainsKey(type.ToString()))
        {
            if (actionTree[type.ToString()] == null)
            {
                actionTree[type.ToString()] = call;
            }
            else
            {
                actionTree[type.ToString()] += call;
            }         
        }
        else
        {
            actionTree.Add(type.ToString(), call);
        }
    }

    /// <summary>
    /// 触发全局事件
    /// </summary>
    /// <param name="type">全局事件类型</param>
    public static void Fire(string type)
    {
        if (actionTree.ContainsKey(type.ToString()))
        {
            if (actionTree[type.ToString()] != null)
            {
                actionTree[type.ToString()]();
            }
        }
        else
        {
            Debug.LogWarning("No Such Event == " + type.ToString());
        }
    }

    /// <summary>
    /// 注销某个全局事件的指定处理方法
    /// </summary>
    public static void UnBind(string type, UnityAction call)
    {
        if (actionTree.ContainsKey(type.ToString()))
        {
            UnityAction tmpCall = actionTree[type.ToString()];
            if (tmpCall != null)
            {
                try
                {
                    tmpCall -= call;
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.ToString());
                }
                if (tmpCall == null)
                {
                    actionTree.Remove(type.ToString());
                }
            }
        }
        else
        {
            Debug.LogError("UnBind Error, no such call be bind == " + call.Method.Name);
        }
    }

    /// <summary>
    /// 注销某个全局事件的所有处理方法
    /// </summary>
    public static void UnBind(string type)
    {
        if (actionTree.ContainsKey(type.ToString()))
        {
            UnityAction tmpCall = actionTree[type.ToString()];
            tmpCall = null;
            actionTree.Remove(type.ToString());
        }
        else
        {
            Debug.LogError("UnBind Error, no such type == " + type.ToString());
        }
    }


    /// <summary>
    /// 注销所有全局事件
    /// </summary>
    public static void UnBindAll()
    {
        actionTree.Clear();
    }
}
