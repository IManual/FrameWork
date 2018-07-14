using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEventBindClick :UIEventBind, IPointerClickHandler
{
    [SerializeField]
    private string eventName;

    private Selectable selectable;

    private UIEventLinkedList linkedList;

    /// <summary>
    /// 获取当前事件的回调列表
    /// </summary>
    /// <returns></returns>
    private UIEventLinkedList GetLinkedList()
    {
        if (linkedList == null)
        {
            linkedList = GetLinkedList(eventName);
        }
        return linkedList;
    }

    /// <summary>
    /// 刷新绑定
    /// </summary>
    protected override void RefreshBind()
    {
        linkedList = base.GetLinkedList(eventName);
    }

    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //判断是否有Selectable组件
        if (selectable!=null && !selectable.interactable)
        {
            return;
        }
        linkedList = GetLinkedList();
        if (linkedList != null)
        {//执行当前事件列表所有方法
            linkedList.Execute(new object[0]);
        }
    }

    /// <summary>
    /// 启动获取自身Selectable组件
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        this.selectable = base.GetComponent<Selectable>();
    }
}
