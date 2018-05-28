using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI管理类
/// </summary>
public class ViewManager : BaseManager<ViewManager> {

    /// <summary>
    /// View名字 和 其所在游戏物体 的字典
    /// </summary>
    Dictionary<string, GameObject> viewDic;

    /// <summary>
    /// 打开的View列表
    /// </summary>
    List<BaseView> openViewList = new List<BaseView>();

    /// <summary>
    /// View名字 和 其对应的View 的字典
    /// </summary>
    Dictionary<string, BaseView> nameViewDic = new Dictionary<string, BaseView>();

    protected override void Init()
    {

    }

    /// <summary>
    /// 根据 nameViewDic里保存的View名字加载所有对应的游戏物体到viewDic中
    /// </summary>
    public void InitViewObject()
    {
        viewDic = new Dictionary<string, GameObject>();
        foreach (var item in nameViewDic)
        {
            viewDic.Add(item.Key, Resources.Load<GameObject>(item.Key));
        }
    }

/// <summary>
/// 打开一个View
/// </summary>
/// <param name="name"></param>
public void Open(string viewName)
    {
        if (viewDic.ContainsKey(viewName))
        {
            //如果界面已创建  直接打开
            if (openViewList.Exists(a => a == nameViewDic[viewName]))
            {
                nameViewDic[viewName].Open();
            }
            else
            {
                //不存在 加载
                GameObject go = GameObject.Instantiate(viewDic[viewName]);
                go.transform.SetParent(Game.Instance.UILayer, false);
                go.transform.localScale = new Vector3(1, 1, 1);
                go.transform.position = Game.Instance.UILayer.position;
                nameViewDic[viewName].PrefabLoadCallBack(go);
                openViewList.Add(nameViewDic[viewName]);
            }
        }
    }

    /// <summary>
    /// 关闭所有View
    /// </summary>
    public void CloseAllView()
    {
        foreach (var item in openViewList)
        {
            item.Close();
        }
    }

    /// <summary>
    /// View是否已打开
    /// </summary>
    public bool CheckOpen(BaseView view)
    {
        return view.IsOpen;
    }

    /// <summary>
    /// 将View从 openViewList 中删除
    /// </summary>
    /// <param name="view"></param>
    public void RemoveOpen(BaseView view)
    {
        openViewList.Remove(view);
    }

    /// <summary>
    /// 将View添加到 nameViewDic
    /// </summary>
    /// <param name="viewName"></param>
    /// <param name="baseView"></param>
    public void RegisterView(string viewName, BaseView baseView)
    {
        this.nameViewDic.Add(viewName, baseView);
    }

    /// <summary>
    /// 将View从两个字典里删除
    /// </summary>
    /// <param name="viewName"></param>
    public void UnRegisterView(string viewName)
    {
        this.nameViewDic.Remove(viewName);
        this.viewDic.Remove(viewName);
    }
}