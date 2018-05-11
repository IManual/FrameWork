using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// View基类
/// </summary>
public class BaseView
{

    /// <summary>
    /// View名字
    /// </summary>
    private string viewName;

    /// <summary>
    /// View的界面
    /// </summary>
    public GameObject viewGameobject;

    /// <summary>
    /// View是否打开
    /// </summary>
    public bool IsOpen { get; private set; }

    /// <summary>
    /// View是否是在界面已创建的情况下打开
    /// </summary>
    public bool IsRealOpen { get; private set; }

    /// <summary>
    /// 计时事件
    /// </summary>
    private TimeEvent deleteTimer;

    private EventTable eventTable;

    private VariableTable variableTable;

    private NameTable nameTable;

    public string bundleName;

    int def_index = 0;
    int? last_index = null;
    int show_index = -1;

    /// <summary>
    /// 初始化界面
    /// </summary>
    /// <param name="vieName"></param>
    public BaseView(string viewName)
    {   
        this.viewName = viewName;
        //将自身名字和对象添加进ViewManger的字典里
        ViewManager.Instance.RegisterView(viewName, this);
        __init();
    }

    /// <summary>
    /// View界面创建完成后的回调
    /// </summary>
    /// <param name="go"></param>
    public virtual void PrefabLoadCallBack(GameObject go)
    {
        viewGameobject = go;
        eventTable = viewGameobject.GetComponent<EventTable>();
        variableTable = viewGameobject.GetComponent<VariableTable>();
        nameTable = viewGameobject.GetComponent<NameTable>(); ;
     
        LoadCallBack();
        GlobalTimeRequest.AddDelayTime(0.02f, OpenCallBack);
    }

    /// <summary>
    /// 界面已创建的情况下打开界面
    /// </summary>
    /// <param name="index"></param>
    public void Open(int? index = null)
    {
        
        IsRealOpen = true;

        //如果未传入index  加载主界面
        if (index == null)
        {
            index = def_index;
        }

        //如果计时事件不为空，就将其置空
        if (deleteTimer != null)
        {
            GlobalTimeRequest.CancleTime(deleteTimer);
            deleteTimer = null;
        }

        if (viewGameobject != null)
        {
            if (!IsOpen)
            {
                //激活界面 并初始化数据
                SetActive(true);
                viewGameobject.transform.localScale = new Vector3(1, 1, 1);
                viewGameobject.transform.position = Game.Instance.UILayer.position;
                //调用回调方法
                OpenCallBack();
            }
            else
            {
                ShowIndexCallBack(index);
            }
        }
    }

    /// <summary>
    /// 设置View界面是否激活
    /// </summary>
    public void SetActive(bool state)
    {
        Debug.Log(state);
        IsOpen = state;
        viewGameobject.SetActive(state);
    }

    public virtual void Flush(params object[] paramsList)
    {
        OnFlush(paramsList);
    }

    /// <summary>
    /// 销毁自身
    /// </summary>
    private void DestroySelf()
    {
        GameObject.Destroy(viewGameobject);
        this.viewGameobject = null;
        ViewManager.Instance.RemoveOpen(this);
        this.ReleaseCallBack();
    }

    /// <summary>
    /// 释放View
    /// </summary>
    public void Release()
    {
        ViewManager.Instance.UnRegisterView(viewName);
        __delete();
    }


    /// <summary>
    /// 对应的标签页Toggle勾选时的触发方法
    /// </summary>
    public void OnToggleChange(int index)
    {
        if (show_index == index) return;
        last_index = index;
        show_index = index;
        ShowIndexCallBack(index);
    }

    public int? GetLastIndex()
    {
        return last_index;
    }

    #region 子类调用

    /// <summary>
    /// 为指定的标签页Toggle组件添加勾选监听
    /// </summary>
    /// <param name="toggle"></param>
    /// <param name="listener"></param>
    /// <param name="param"></param>
    public void AddToggleValueChangedListener(GameObject toggle, UnityAction<int> listener,int tabIndex)
    {
        toggle.GetComponent<Toggle>().onValueChanged.AddListener((b) => {
            if (show_index == tabIndex) return;
            if (b) listener(tabIndex);
        });
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

    /// <summary>
    /// 往EventTable的字典里添加事件监听
    /// </summary>
    public void ListenEvent(string eventName, UnityAction listener)
    {
        try
        {
            eventTable.ListenEvent(eventName, listener);
        }
        catch (Exception e)
        {

            Debug.LogError(e.ToString());
        }      
    }

    /// <summary>
    /// 取消指定事件的监听
    /// </summary>
    public void ClearEvent(string eventName)
    {
        if (eventTable != null)
            eventTable.ClearEvent(eventName);
    }

    /// <summary>
    /// 取消所有事件的监听
    /// </summary>
    public void ClearAllEvent()
    {
        if (eventTable != null)
            eventTable.ClearAllEvent();
    }

    /// <summary>
    /// 关闭View界面
    /// </summary>
    public void Close()
    {
        viewGameobject.SetActive(false);
        CloseCallBack();
        deleteTimer = GlobalTimeRequest.AddDelayTime(5, DestroySelf);
    }

    #endregion

    #region 需要子类重写的生命周期方法

    /// <summary>
    /// 预制件加载完成时调用
    /// </summary>
    public virtual void __init()
    {

    }

    public virtual void __delete()
    {

    }

    public virtual void LoadCallBack()
    {

    }

    public virtual void ReleaseCallBack()
    {

    }

    public virtual void OpenCallBack()
    {

    }

    public virtual void CloseCallBack()
    {

    }

    public virtual void OnFlush(params object[] paramsList)
    {

    }

    public virtual void ShowIndexCallBack(int? index)
    {

    }
    #endregion
}
