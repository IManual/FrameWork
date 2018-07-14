using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// UI事件配置表
/// </summary>
public class UIEventTable : BaseBehaviour
{
    [SerializeField, Tooltip("The event list.")]
    private string[] events;

    public string[] Events
    {
        get { return this.events; }
    }

    private Dictionary<string, UIEventLinkedList> eventMap;       //string和事件(委托链)对应关系

    //一个string对应一个链表
    private Dictionary<string, LinkedList<Component>> eventBindMap = new Dictionary<string, LinkedList<Component>>(StringComparer.Ordinal);     //string和绑定的所有组件对应关系

    /// <summary>
    /// 获取事件表
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, UIEventLinkedList> GetEventMap()
    {
        if (eventMap == null)
        {
            eventMap = new Dictionary<string, UIEventLinkedList>(StringComparer.Ordinal);
            string[] array = this.events;
            for (int i = 0; i < array.Length; i++)
            {
                if (!eventMap.ContainsKey(events[i]))
                {
                    eventMap.Add(events[i], new UIEventLinkedList());
                }
            }

        }
        return eventMap;
    }

    public void Sort()
    {
        Array.Sort<string>(events);
    }

    /// <summary>
    /// 监听事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="call"></param>
    /// <returns></returns>
    public SignalHandle ListenEvent(string eventName, SignalDelegate call)
    {
        UIEventLinkedList linkedList;
        if (!GetEventMap().TryGetValue(eventName, out linkedList))
        {
            Debug.LogErrorFormat("{0} is trying to listen event {1}, but it does not existed.", new object[]
                {
                    name,
                    eventName
                });
            return new SignalHandle(null, null);
        }
        return linkedList.GetHandle(call);
    }

    /// <summary>
    /// 清除当前界面所有事件
    /// </summary>
    public void ClearAllEvents()
    {
        foreach (var item in GetEventMap())
        {
            item.Value.Clear();
        }
    }

    public void ClearEvent(string eventName)
    {
        UIEventLinkedList result;
        if(!GetEventMap().TryGetValue(eventName, out result))
        {
            Debug.LogErrorFormat("{0} is trying to clear event {1}, but it does not existed.",
                new object[] { name, eventName });
        }
        else if (result != null)
        {
            result.Clear();
        }
    }

    /// <summary>
    /// 查找引用当前点击事件的所有组件
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns></returns>
    public ICollection<Component> FindReferenced(string eventName)
    {
        if (string.IsNullOrEmpty(eventName))
        {
            return null;
        }
        LinkedList<Component> result;
        if (!eventBindMap.TryGetValue(eventName, out result))
        {
            return null;
        }
        return result;
    }

    /// <summary>
    /// 给eventName的component链加一个节点
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public LinkedListNode<Component> AddNode(string eventName, Component value)
    {
        LinkedList<Component> linkedList;
        //如果当前event没有对应的linkedList 创建一个
        if (!eventBindMap.TryGetValue(eventName, out linkedList))
        {
            linkedList = new LinkedList<Component>();
            eventBindMap.Add(eventName, linkedList);
        }
        //吧value加载linkedList最后
        return linkedList.AddLast(value);
    }

    /// <summary>
    /// 删除eventName的component链上的一个节点node
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="node"></param>
    public void RemoveNode(string eventName, LinkedListNode<Component> node)
    {
        LinkedList<Component> linkedList;
        if(eventBindMap.TryGetValue(eventName,out linkedList))
        {
            linkedList.Remove(node);
        }
    }

    /// <summary>
    /// 根据eventName获取对应点击事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns></returns>
    public UIEventLinkedList GetHandle(string eventName)
    {
        if (string.IsNullOrEmpty(eventName)) { return null; }
        UIEventLinkedList result;
        if (!GetEventMap().TryGetValue(eventName, out result))
        {
            return null;
        }
        return result;
    }

    protected override void OnValidate()
    {
        eventMap = null; //当前table的值有修改时将eventMap置空
    }
}