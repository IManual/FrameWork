using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC入口
/// </summary>
public static class MVCEntry {

    /// <summary>
    /// 保存所有Ctrl的列表
    /// </summary>
    static List<BaseCtrl> CtrlList = new List<BaseCtrl>();

    /// <summary>
    /// 初始化Ctrl
    /// </summary>
    public static void __init()
    {
        CreateCtrls();
    }

    /// <summary>
    /// 创建并初始化各Ctrl 对应的View和Data也会被同步创建
    /// </summary>
    private static void CreateCtrls()
    {
        CtrlList.Add(new MainCtrl());
    }

    /// <summary>
    /// 删除列表里的所有Ctrl
    /// </summary>
    public static void __delete()
    {
        for (int i = 0; i < CtrlList.Count; i++)
        {
            CtrlList[i].DeleteMe();
        }
    }
}
