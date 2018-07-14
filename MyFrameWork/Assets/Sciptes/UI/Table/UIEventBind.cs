using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UI事件绑定类
/// 这个脚本把UI事件绑定到游戏物体上（指定哪个游戏物体来触发UI事件，事件触发接口由子类实现）
/// </summary>
public abstract class UIEventBind : BaseBehaviour
{
    [SerializeField, Tooltip("The event table for this bind.")]
    private UIEventTable eventTable;

    /// <summary>
    /// eventName对应组件的映射关系
    /// </summary>
    private Dictionary<string, LinkedListNode<Component>> linkedMap = new Dictionary<string, LinkedListNode<Component>>(StringComparer.Ordinal);

    public UIEventTable EventTable
    {
        get;
        private set;
    }

    /// <summary>
    /// 获取eventName对应的事件列表
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns></returns>
    public UIEventLinkedList GetLinkedList(string eventName)
    {
        if(EventTable != null)
        {//拿到当前eventnName对应的事件处理方法
            UIEventLinkedList handle = EventTable.GetHandle(eventName);
            if (handle != null)
            {
                LinkedListNode<Component> linkedListNode;
                //拿到当前eventName绑定的一个组件
                if (linkedMap.TryGetValue(eventName, out linkedListNode))
                {
                    //删除这个节点
                    EventTable.RemoveNode(eventName, linkedListNode);
                    linkedMap.Remove(eventName);
                }
                //重新创建一个节点 将自身添加进去
                linkedListNode = EventTable.AddNode(eventName, this);
                linkedMap.Add(eventName, linkedListNode);
            }
            return handle;
        }
        return null;
    }

    public void GetTable()
    {
        if(this.eventTable == null)
        {
            this.eventTable = this.GetComponentInParentHard<UIEventTable>();
        }
        this.EventTable = this.eventTable;
    }

    protected abstract void RefreshBind();

    protected override void Awake()
    {
        GetTable();
    }

    protected override void OnValidate()
    {
        UnBindAllComponent();
        GetTable();
        RefreshBind();
    }

    /// <summary>
    /// 释放所有eventName绑定的component
    /// </summary>
    private void UnBindAllComponent()
    {
        if(EventTable != null)
        {
            foreach(KeyValuePair<string, LinkedListNode<Component>> current in linkedMap)
            {
                EventTable.RemoveNode(current.Key, current.Value);
            }
        }
        linkedMap.Clear();
    }

    protected override void OnDestroy()
    {
        UnBindAllComponent();
    }
}
