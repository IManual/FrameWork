using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UI事件绑定类
/// 这个脚本把UI事件绑定到游戏物体上（指定哪个游戏物体来触发UI事件，事件触发接口由子类实现）
/// </summary>
public class EventBind :BaseBehaviour {

    /// <summary>
    /// UI事件配置表
    /// </summary>
    public EventTable table;

    /// <summary>
    /// UI事件参数
    /// </summary>
    [HideInInspector]
    public UIEvent bindEvent;

    [HideInInspector]
    public int index;
   
}
