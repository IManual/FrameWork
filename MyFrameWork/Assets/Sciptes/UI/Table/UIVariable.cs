using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// 变量表单个变量
/// </summary>
[Serializable]
public class UIVariable
{
    #region Property

    /// <summary>
    /// 变量名
    /// </summary>
    [SerializeField]
    private string name;

    [SerializeField]
    private UIVariableType type;

    [SerializeField]
    private bool booleanValue;

    [SerializeField]
    private long intergerValue;

    [SerializeField]
    private float floatValue;

    [SerializeField]
    private string stringValue;

    [SerializeField]
    private AssetID assetValue;

    #endregion

    /// <summary>
    /// 当前变量所绑定的bind脚本列表
    /// </summary>
    //private List<UIVariableBind> variableBindList = new List<UIVariableBind>();

    //值改变回调
    private Action onValueChange;

    //值初始化回调
    private Action onValueInitialized;

    //[CompilerGenerated]
    //private static Predicate<UIVariableBind> bindList;

    /// <summary>
    /// 变量改变
    /// </summary>
    public event Action OnValueChanged
    {
        // add remove事件访问器
        [MethodImpl(MethodImplOptions.Synchronized)]
        add
        {
            this.onValueChange = (Action)Delegate.Combine(this.onValueChange, value);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        remove
        {
            this.onValueChange = (Action)Delegate.Remove(this.onValueChange, value);
        }
    }

    /// <summary>
    /// 变量初始化
    /// </summary>
    public event Action OnValueInitialized
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        add
        {
            this.onValueInitialized = (Action)Delegate.Combine(this.onValueInitialized, value);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        remove
        {
            this.onValueInitialized = (Action)Delegate.Remove(this.onValueInitialized, value);
        }
    }

    /// <summary>
    /// 当前变量的变量名
    /// </summary>
    public string Name
    {
        get { return this.name; }
    }

    /// <summary>
    /// 当前变量的类型
    /// </summary>
    public UIVariableType Type
    {
        get
        {
            return this.type;
        }
    }

    /// <summary>
    /// 当前变量的值
    /// </summary>
    public object ValueObject
    {
        //根据当前类型  返回所对应的值
        get
        {
            switch (this.type)
            {
                case UIVariableType.Boolean:
                    return this.booleanValue;
                case UIVariableType.Float:
                    return this.floatValue;
                case UIVariableType.Interger:
                    return this.intergerValue;
                case UIVariableType.String:
                    return this.stringValue;
                case UIVariableType.Asset:
                    return this.assetValue;
                default:
                    return null;
            }
        }
    }

    #region GetValue

    public bool GetBoolean()
    {
        return this.booleanValue;
    }

    public int GeyInteger()
    {
        return (int)this.intergerValue;
    }

    public float GetFloat()
    {
        return this.floatValue;
    }

    public string GetString()
    {
        return this.stringValue;
    }

    public AssetID GetAsset()
    {
        return this.assetValue;
    }

    #endregion

    #region InitValue

    public void InitBoolean(bool value)
    {
        if (this.booleanValue != value)
        {
            this.booleanValue = value;
            this.ValueInitialized();
        }
    }

    public void InitInteger(long value)
    {
        if (this.intergerValue != value)
        {
            this.intergerValue = value;
            this.ValueInitialized();
        }
    }

    public void InitFloat(float value)
    {
        if (this.floatValue != value)
        {
            this.floatValue = value;
            this.ValueInitialized();
        }
    }

    public void InitString(string value)
    {
        if (this.stringValue != value)
        {
            this.stringValue = value;
            this.ValueInitialized();
        }
    }

    public void InitValue(bool value)
    {
        switch (this.type)
        {
            case UIVariableType.Boolean:
                this.InitBoolean(value);
                break;
            case UIVariableType.Interger:
                this.InitInteger((!value) ? 0L : 1L);
                break;
            case UIVariableType.Float:
                this.InitFloat((!value) ? 0f : 1f);
                break;
            case UIVariableType.String:
                this.InitString(value.ToString());
                break;
        }
    }

    public void InitValue(long value)
    {
        switch (this.type)
        {
            case UIVariableType.Boolean:
                this.InitBoolean(value != 0L);
                break;
            case UIVariableType.Interger:
                this.InitInteger(value);
                break;
            case UIVariableType.Float:
                this.InitFloat((float)value);
                break;
            case UIVariableType.String:
                this.InitString(value.ToString());
                break;
        }
    }

    public void InitValue(float value)
    {
        switch (this.type)
        {
            case UIVariableType.Boolean:
                this.InitBoolean(!Mathf.Approximately(value, 0f));
                break;
            case UIVariableType.Interger:
                this.InitInteger((long)value);
                break;
            case UIVariableType.Float:
                this.InitFloat(value);
                break;
            case UIVariableType.String:
                this.InitString(value.ToString());
                break;
        }
    }

    public void InitValue(string value)
    {
        switch (this.type)
        {
            case UIVariableType.Boolean:
                this.InitBoolean(bool.Parse(value));
                break;
            case UIVariableType.Interger:
                this.InitInteger(long.Parse(value));
                break;
            case UIVariableType.Float:
                this.InitFloat(float.Parse(value));
                break;
            case UIVariableType.String:
                this.InitString(value);
                break;
        }
    }

    #endregion

    #region SetValue

    public void SetBoolean(bool value)
    {
        if (this.booleanValue != value)
        {
            Debug.Log(name);
            Debug.Log(value);
            this.booleanValue = value;
            this.ValueChange();
        }
    }

    public void SetInteger(long value)
    {
        if (this.intergerValue != value)
        {
            this.intergerValue = value;
            this.ValueChange();
        }
    }

    public void SetFloat(float value)
    {
        if (this.floatValue != value)
        {
            this.floatValue = value;
            this.ValueChange();
        }
    }

    public void SetString(string value)
    {
        if (this.stringValue != value)
        {
            this.stringValue = value;
            this.ValueChange();
        }
    }

    public void SetValue(bool value)
    {
        switch (this.type)
        {
            case UIVariableType.Boolean:
                this.SetBoolean(value);
                break;
            case UIVariableType.Interger:
                this.SetInteger((!value) ? 0L : 1L);
                break;
            case UIVariableType.Float:
                this.SetFloat((!value) ? 0f : 1f);
                break;
            case UIVariableType.String:
                this.SetString(value.ToString());
                break;
        }
    }

    public void SetValue(long value)
    {
        switch (this.type)
        {
            case UIVariableType.Boolean:
                this.SetBoolean(value != 0L);
                break;
            case UIVariableType.Interger:
                this.SetInteger(value);
                break;
            case UIVariableType.Float:
                this.SetFloat((float)value);
                break;
            case UIVariableType.String:
                this.SetString(value.ToString());
                break;
        }
    }

    public void SetValue(float value)
    {
        switch (this.type)
        {
            case UIVariableType.Boolean:
                this.SetBoolean(!Mathf.Approximately(value, 0f));
                break;
            case UIVariableType.Interger:
                this.SetInteger((long)value);
                break;
            case UIVariableType.Float:
                this.SetFloat(value);
                break;
            case UIVariableType.String:
                this.SetString(value.ToString());
                break;
        }
    }

    public void SetValue(string value)
    {
        switch (this.type)
        {
            case UIVariableType.Boolean:
                this.SetBoolean(bool.Parse(value));
                break;
            case UIVariableType.Interger:
                this.SetInteger(long.Parse(value));
                break;
            case UIVariableType.Float:
                this.SetFloat(float.Parse(value));
                break;
            case UIVariableType.String:
                this.SetString(value);
                break;
        }
    }

    public void SetAsset(string bundle, string asset)
    {
        this.SetAsset(new AssetID(bundle, asset));
    }

    public void SetAsset(AssetID assetID)
    {
        if (!this.assetValue.Equals(assetID))
        {
            this.assetValue = assetID;
            this.ValueChange();
        }
    }

    #endregion

    public void ValueChange()
    {
        if (this.onValueChange != null)
        {
            this.onValueChange();
        }
    }

    public void ValueInitialized()
    {
        if (this.onValueInitialized != null)
        {
            this.onValueInitialized();
        }
    }

    /// <summary>
    /// 重置asset
    /// </summary>
    public void ResetAsset()
    {
        this.SetAsset(AssetID.Empty);
    }

    #region 多余代码
    //public void AddBind(UIVariableBind bind)
    //{
    //    if (this.variableBindList.IndexOf(bind) == -1)
    //    {
    //        this.variableBindList.Add(bind);
    //    }
    //}

    //public void RemoveBind(UIVariableBind bind)
    //{
    //    this.variableBindList.Remove(bind);
    //}

    //internal void Fun0()
    //{
    //    List<UIVariableBind> arg_23_0 = this.variableBindList;
    //    if (UIVariable.bindList == null)
    //    {
    //        UIVariable.bindList = new Predicate<UIVariableBind>(UIVariable.IsNull);
    //    }
    //    arg_23_0.RemoveAll(UIVariable.bindList);
    //}

    //internal void Fun()
    //{
    //    switch (this.type)
    //    {
    //        case UIVariableType.Boolean:
    //            this.intergerValue = 0L;
    //            this.floatValue = 0f;
    //            this.stringValue = null;
    //            this.assetValue = default(AssetID);
    //            break;
    //        case UIVariableType.Interger:
    //            this.booleanValue = false;
    //            this.floatValue = 0f;
    //            this.stringValue = null;
    //            this.assetValue = default(AssetID);
    //            break;
    //        case UIVariableType.Float:
    //            this.intergerValue = 0L;
    //            this.booleanValue = false;
    //            this.stringValue = null;
    //            this.assetValue = default(AssetID);
    //            break;
    //        case UIVariableType.String:
    //            this.intergerValue = 0L;
    //            this.floatValue = 0f;
    //            this.assetValue = default(AssetID);
    //            break;
    //        case UIVariableType.Asset:
    //            this.intergerValue = 0L;
    //            this.floatValue = 0f;
    //            this.stringValue = null;
    //            this.booleanValue = false;
    //            break;
    //    }
    //}


    //[CompilerGenerated]
    //private static bool IsNull(UIVariableBind uIVariableBind)
    //{
    //    return uIVariableBind == null;
    //}
    #endregion
}

