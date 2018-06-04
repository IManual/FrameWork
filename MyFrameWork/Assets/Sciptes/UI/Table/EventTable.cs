using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// UI事件配置表
/// </summary>
public class EventTable : BaseBehaviour {

    /// <summary>
    /// 事件参数数组
    /// </summary>
    [SerializeField]
    public UIEvent[] events;

    /// <summary>
    /// 事件名字与事件参数的字典
    /// </summary>
    Dictionary<string, UIEvent> eventDic = new Dictionary<string, UIEvent>();

    protected override void Awake()
    {      
        //根据事件参数数组里的数据为字典添加键值对
        foreach (var item in events)
        {
            item.ClickEventHandler = null;
            eventDic.Add(item.eventName, item);
        }
    }

    /// <summary>
    /// 添加事件监听
    /// </summary>
    public void ListenEvent(string eventName, UnityAction call)
    {
        if (eventDic.ContainsKey(eventName))
        {
            if (eventDic[eventName].ClickEventHandler != null)
            {
                eventDic[eventName].ClickEventHandler += call;
            }
            else
            {
                eventDic[eventName].ClickEventHandler = call;
            }
        }
        else
        {
            Debug.LogError("no such event,check it out!");
        }
    }

    /// <summary>
    /// 获取指定事件对应的处理方法
    /// </summary>
    public UnityAction GetEventHandler(string eventName)
    {
        return eventDic[eventName].ClickEventHandler;
    }

    /// <summary>
    /// 清除事件
    /// </summary>
    public void ClearEvent(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            eventDic.Remove(eventName);
        }
    }

    /// <summary>
    /// 清除所有事件
    /// </summary>
    public void ClearAllEvent()
    {
        eventDic.Clear();
    }

    protected override void OnDestroy()
    {
        foreach (var item in events)
        {
            item.ClickEventHandler = null;
        }
    }
}

/// <summary>
/// UI事件参数类
/// </summary>
[Serializable]
public class UIEvent
{
    /// <summary>
    /// 事件名字
    /// </summary>
    public string eventName;

    public GameObject btnObj;

    [HideInInspector]
    public UnityAction ClickEventHandler;
}