using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[AddComponentMenu("Nirvana/UI/Bind/Variable Bind Text"), RequireComponent(typeof(Text))]
public class UIVariableBindText : UIVariableBind
{
    [Delayed, SerializeField, TextArea(0, 10)]
    public string format;

    [VariableName(UIVariableType.Boolean, UIVariableType.Interger, UIVariableType.Float, UIVariableType.String), SerializeField]
    private string[] paramBinds;                //绑定的变量参数列表

    public string[] ParamBinds
    {
        get
        {
            return paramBinds;
        }

        set
        {
            paramBinds = value;
        }
    }

    private Text UIText;

    private UIVariable[] variableList;          //绑定的变量列表

    public string Format
    {
        get { return this.format; }
        set { if (this.format != value) { this.format = value; this.SetFormat(); } }
    }

    protected override void Awake()
    {
        base.Awake();
        this.UIText = base.GetComponent<Text>();
        if (this.UIText == null)
        {
            return;
        }
    }

    /// <summary>
    /// 外部修改变量绑定时调用刷新文本
    /// </summary>
    public void SetFormat()
    {
        if (this.UIText == null)
        {
            this.UIText = base.GetComponent<Text>();
        }
        if (this.UIText || this.paramBinds == null || this.variableList == null)
        {
            return;
        }
        if (string.IsNullOrEmpty(this.format))
        {
            if (this.paramBinds.Length > 0)
            {
                UIVariable uIVariable = this.variableList[0];
                if (uIVariable != null)
                {
                    object valueObject = uIVariable.ValueObject;
                    if (valueObject != null)
                    {
                        this.UIText.text = valueObject.ToString();
                    }
                }
            }
        }
        else
        {
            object[] array = new object[this.paramBinds.Length];
            for (int i = 0; i < this.paramBinds.Length; i++)
            {
                UIVariable uIVariable2 = this.variableList[i];
                if (uIVariable2 != null)
                {
                    array[i] = uIVariable2.ValueObject;
                }
            }
            try
            {
                this.UIText.text = string.Format(this.format, array);
            }
            catch (FormatException ex)
            {
                if (Application.isPlaying)
                {
                    Debug.Log(ex.Message, this);
                }
            }
        }
    }

    //脚本挂载时调用
    public override void BindVariables()
    {
        Debug.Log("BindVariable");
        Assert.IsNull<UIVariable[]>(this.variableList);
        if (this.paramBinds != null && this.paramBinds.Length > 0)
        {
            this.variableList = new UIVariable[this.paramBinds.Length];
            for (int i = 0; i < this.paramBinds.Length; i++)
            {
                //paramBinds存的是变量的名称
                string text = this.paramBinds[i];
                if (!string.IsNullOrEmpty(text))
                {
                    UIVariable uIVariable = base.FindVariable(text); 
                    if (uIVariable == null)
                    {
                        Debug.Log(name + "can not find a variable " + text);
                    }
                    else
                    {
                        uIVariable.OnValueInitialized += new Action(this.SetFormat);
                        uIVariable.OnValueChanged += new Action(this.SetFormat);
                        uIVariable.AddBind(this);
                        this.variableList[i] = uIVariable;
                    }
                }
            }
            this.SetFormat();
        }
    }

    public override void UnbindVariables()
    {
        if (this.variableList != null)
        {
            UIVariable[] array = this.variableList;
            for (int i = 0; i < array.Length; i++)
            {
                UIVariable uIVariable = array[i];
                if (uIVariable != null)
                {
                    uIVariable.OnValueChanged -= new Action(this.SetFormat);
                    uIVariable.OnValueInitialized -= new Action(this.SetFormat);
                    uIVariable.RemoveBind(this);
                }
            }
            this.variableList = null;
        }
    }
}
