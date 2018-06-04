using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI变量参数类
/// </summary>
[Serializable]
public class UIVariable
{
    /// <summary>
    /// 变量名
    /// </summary>
    [SerializeField]
    public string name;

    [SerializeField]
    private bool booleanValue;

    [SerializeField]
    private long interValue;

    [SerializeField]
    private float floatValue;

    [SerializeField]
    private string stringValue;

    /// <summary>
    /// 变量类型
    /// </summary>
    public UIVariableType type;

    /// <summary>
    /// 变量值
    /// </summary>
    public object value;

    [HideInInspector]
    public List<GameObject> obList;

    private List<VariableBind> variableBindList = new List<VariableBind>();

    private Action onValueChange;

    private Action onVlaueInitialized;

    private static Predicate<VariableBind> bindList;

    //变量改变
    public event Action OnValueChanged
    {
        // add remove事件访问器
        add
        {
            this.onValueChange = (Action)Delegate.Combine(this.onValueChange, value);
        }
        remove
        {
            this.onValueChange = (Action)Delegate.Remove(this.onValueChange, value);
        }
    }

    //变量赋值
    public event Action OnVlaueInitialized
    {
        add
        {
            this.onVlaueInitialized = (Action)Delegate.Combine(this.onVlaueInitialized, value);
        }
        remove
        {
            this.onVlaueInitialized = (Action)Delegate.Remove(this.onVlaueInitialized, value);
        }
    }

    public object ValueObject
    {
        //根据当前类型  返回所对应的值
        get
        {
            switch (this.type)
            {
                case UIVariableType.Bollean:
                    return this.booleanValue;
                case UIVariableType.Float:
                    return this.floatValue;
                case UIVariableType.Interger:
                    return this.interValue;
                case UIVariableType.String:
                    return this.stringValue;
                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// 设置变量值
    /// </summary>
    public void SetValue(object value)
    {
        this.value = value;
        //触发变量改变事件
        TableToValue.FireEvent(this.name, this);
    }
}

