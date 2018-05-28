using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Render基类 子界面用
/// </summary>
public class BaseRender {

    /// <summary>
    /// 该类对应界面的游戏物体
    /// </summary>
    GameObject viewGameObject;

    NameTable nameTable;

    EventTable eventTable;

    VariableTable variableTable;

    public BaseRender(GameObject instance)
    {
        viewGameObject = instance;
        nameTable = viewGameObject.GetComponent<NameTable>();
        eventTable = viewGameObject.GetComponent<EventTable>();
        variableTable = viewGameObject.GetComponent<VariableTable>();
        __init();
    }

    

    /// <summary>
    /// 从NameTable的字典里寻找UI相关的游戏物体
    /// </summary>
    public GameObject FindObj(string objName)
    {
        try
        {
            return nameTable.FindObj(objName);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return null;
        }
    }

    /// <summary>
    /// 往EventTable的字典里添加事件监听
    /// </summary>
    public void ListenEvent(string eventName,UnityAction call)
    {
        try
        {
            eventTable.ListenEvent(eventName, call);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    /// <summary>
    /// 从VariableTable的数组里寻找UI相关的变量
    /// </summary>
    public UIVariable FindVariable(string variableName)
    {
        try
        {
            return variableTable.FindVariable(variableName);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return null;
        }
    }


    #region 子类继承
    /// <summary>
    /// 类似于BaseView的LoadCallBack
    /// </summary>
    public virtual void __init() { }

    public virtual void __delete() { }

    public virtual void OpenCallBack() { }

    public virtual void CloseCallBack() { }

    public virtual void OnFlush(params object[] paramsList) { }

    public virtual void Flush(params object[] paramsList)
    {
        OnFlush(paramsList);
    }
    #endregion
}
