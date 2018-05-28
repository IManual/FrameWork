using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Ctrl基类
/// </summary>
public class BaseCtrl {

    private BaseCtrl instance;

    public BaseCtrl Instance
    {
        get
        {
            if (instance == null)
            {              
                instance = new BaseCtrl();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public BaseCtrl()
    {
        this.__init();
    }

    /// <summary>
    /// 在这里对View和Data进行对象创建
    /// </summary>
    public virtual void __init() { }

    /// <summary>
    /// 在这里对View和Data进行对象释放
    /// </summary>
    public virtual void __delete() { }


    public void DeleteMe()
    {
        __delete();
    }

   

    /// <summary>
    /// 绑定全局事件和对应的处理方法
    /// </summary>
    public void BindGlobalEvent(string evetnType,UnityAction action)
    {
        GlobalEventSystem.Bind(evetnType, action);
    }
    /// <summary>
    /// 注销单个全局事件的指定处理方法
    /// </summary>
    public void UnBind(string evetnType, UnityAction action)
    {
        GlobalEventSystem.UnBind(evetnType, action);
    }
    /// <summary>
    /// 注销某个全局事件的所有处理方法
    /// </summary>
    public void UnBind(string evetnType)
    {
        GlobalEventSystem.UnBind(evetnType);
    }

    public void Fire(string evetnType)
    {
        GlobalEventSystem.Fire(evetnType);
    }

    /// <summary>
    /// 往UI管理器模块注册消息与对应处理方法
    /// </summary>
    /// <param name="tmpMsg">消息</param>
    /// <param name="handler">处理方法</param>
    public void Register(MsgBase tmpMsg, Action<MsgBase> handler)
    {
        ViewManager.Instance.Register(tmpMsg, handler);
    }

    /// <summary>
    /// 解除消息与处理方法的对应
    /// </summary>
    /// <param name="tmpMsg">消息</param>
    /// <param name="handler">处理方法</param>
    public void UnRegister(MsgBase tmpMsg, Action<MsgBase> handler)
    {
        ViewManager.Instance.UnRegister(tmpMsg, handler);
    }

    /// <summary>
    /// 解除指定消息对应的所有处理方法
    /// </summary>
    /// <param name="tmpMsg">消息</param>
    public void UnRegister(MsgBase tmpMsg)
    {
        ViewManager.Instance.UnRegister(tmpMsg);
    }
}

