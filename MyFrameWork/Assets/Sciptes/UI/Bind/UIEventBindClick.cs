using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIEventBindClick :UIEventBind, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //通过UI事件配置表获取到该脚本要触发的UI事件对应的处理方法
        UnityAction call = table.GetEventHandler(bindEvent.eventName);
        if (call != null)
        {
            call();
        }
    }
}
