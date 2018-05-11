using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI界面配置表
/// </summary>
public class NameTable : BaseBehaviour {

    /// <summary>
    /// 界面参数数组 
    /// </summary>
    [SerializeField]
    private UIComponent[] component;

    /// <summary>
    /// UI界面名字和对应游戏物体 的字典
    /// </summary>
    Dictionary<string, GameObject> obDic = new Dictionary<string, GameObject>();

    protected override void Awake()
    {
        //根据界面参数数组的数据为字典添加键值对
        for (int i = 0; i < component.Length; i++)
        {
            obDic.Add(component[i].name, component[i].gameObject);
        }
    }
    
    /// <summary>
    /// 根据界面名字找到对应游戏物体
    /// </summary>
    public GameObject FindObj(string name)
    {
        if (obDic.ContainsKey(name))
        {
            return obDic[name];
        }
        else
        {
            Debug.LogError("the name '" + name + "'is not contain in this nameTable");
            return null;
        }
    }

}

/// <summary>
/// UI界面参数类
/// </summary>
[Serializable]
public class UIComponent {
    /// <summary>
    /// 界面名字
    /// </summary>
    public string name;

    /// <summary>
    /// 界面对应的游戏物体
    /// </summary>
    public GameObject gameObject;
}
