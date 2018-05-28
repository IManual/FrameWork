using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// UI变量配置表
/// </summary>
[LuaCallCSharp]
public class VariableTable : BaseBehaviour {

    /// <summary>
    /// 变量参数数组
    /// </summary>
    [SerializeField]
    public UIVariable[] component;

    /// <summary>
    /// 变量名字与变量类型的字典
    /// </summary>
    public Dictionary<string, VariableTYPE> variableDic;

    protected override void Awake()
    {
        //根据变量参数数组里的数据为字典添加键值对
        variableDic = new Dictionary<string, VariableTYPE>();
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

/// <summary>
/// UI变量参数类
/// </summary>
[Serializable]
public class UIVariable
{
    /// <summary>
    /// 变量名
    /// </summary>
    public string name;

    /// <summary>
    /// 变量类型
    /// </summary>
    public VariableTYPE type;
    
    /// <summary>
    /// 变量值
    /// </summary>
    public object value;

    [HideInInspector]
    public List<GameObject> obList;

    /// <summary>
    /// 设置变量值
    /// </summary>
    public void SetVlaue(object value)
    {
        this.value = value;
        //触发变量改变事件
        TableToValue.FireEvent(this.name, this);
    }
}

public enum VariableTYPE
{
    String,
    Bool,
    Int,
}
