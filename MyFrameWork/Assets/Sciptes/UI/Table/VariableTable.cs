using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI变量配置表
/// </summary>
public class VariableTable : BaseBehaviour {

    /// <summary>
    /// 变量参数数组
    /// </summary>
    [SerializeField]
    public UIVariable[] component;

    /// <summary>
    /// 变量名字与变量类型的字典
    /// </summary>
    public Dictionary<string, UIVariableType> variableDic;

    protected override void Awake()
    {
        //根据变量参数数组里的数据为字典添加键值对
        variableDic = new Dictionary<string, UIVariableType>();
        for (int i = 0; i < component.Length; i++)
        {
            variableDic.Add(component[i].name, component[i].type);
        }
    }

    /// <summary>
    /// 根据变量名字找到对应变量参数对象
    /// </summary>
    public UIVariable FindVariable(string name)
    {
        foreach (var item in component)
        {
            if(item.name == name)
            {
                return item;
            }
        }
        return null;
    }
}
