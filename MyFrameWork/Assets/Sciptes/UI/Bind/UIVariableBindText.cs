using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[AddComponentMenu("UI/Bind/Variable Bind Text"), RequireComponent(typeof(Text))]
public class UIVariableBindText : UIVariableBind
{
    /// <summary>
    /// 当前text的格式
    /// </summary>
    [Delayed, SerializeField, TextArea(0, 10)]
    public string format;

    /// <summary>
    /// 当前的text格式 设置时调用刷新方法SetFormat()
    /// </summary>
    public string Format
    {
        get { return this.format; }
        set { if (this.format != value) { this.format = value; this.SetFormat(); } }
    }

    /// <summary>
    /// 绑定的变量名（string名称）列表
    /// </summary>
    [SerializeField]
    private string[] paramBinds;

    /// <summary>
    /// 当前的Text组件
    /// </summary>
    private Text UIText;

    /// <summary>
    /// 当前绑定的变量列表
    /// </summary>
    private UIVariable[] variableList;

    /// <summary>
    /// 挂载时启动 查找到当前Text组件 执行父类初始化方法 查找variabletable 执行当前Bind方法
    /// </summary>
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
    /// 外部修改绑定的变量列表时调用刷新文本
    /// </summary>
    public void SetFormat()
    {
        if (UIText == null)
        {
            UIText = base.GetComponent<Text>();
        }
        if (!(UIText == null) && paramBinds != null && variableList != null)
        {
            if (string.IsNullOrEmpty(format))
            {
                if (paramBinds.Length > 0)
                {
                    UIVariable uIVariable = variableList[0];
                    if (uIVariable != null)
                    {
                        object valueObject = uIVariable.ValueObject;
                        if (valueObject != null)
                        {
                            UIText.text = valueObject.ToString();
                        }
                    }
                }
            }
            else
            {
                foreach (var item in variableList)
                {
                    //Debug.Log(item.Name + "------------" + item.GetHashCode());
                }
                object[] array = new object[paramBinds.Length];
                for (int i = 0; i < paramBinds.Length; i++)
                {
                    UIVariable uIVariable2 = variableList[i];
                    if (uIVariable2 != null)
                    {
                        array[i] = uIVariable2.ValueObject;
                    }
                }
                try
                {
                    UIText.text = string.Format(format, array);
                }
                catch(FormatException ex)
                {
                    if (Application.isPlaying)
                    {
                        Debug.LogError(ex.Message, this);
                    }
                }
            }
            return;
        }

            //try
            //{//尝试按照format格式进行组合
            //    tmp = this.format;
            //    if (Regex.Matches(tmp, "{").Count % 2 == 1 && Regex.Matches(tmp, "{").Count != Regex.Matches(tmp, "}").Count)
            //    {
            //        tmp = tmp.Replace("{", "{{");                 
            //    }
            //    this.UIText.text = string.Format(tmp, array);
            //}
            //catch (FormatException ex)
            //{//如果失败 抛出异常
            //    if (Application.isPlaying)
            //    {
            //        Debug.Log(ex.Message, this);
            //    }
            //}
    }
    //string tmp = "";
    /// <summary>
    /// 当前脚本挂载时调用一次  父类Init方法可调用当前方法
    /// </summary>
    protected override void BindVariables()
    {
        //如果当前绑定变量名列表有值
        if (this.paramBinds != null && this.paramBinds.Length > 0)
        {
            this.variableList = new UIVariable[this.paramBinds.Length];
            for (int i = 0; i < this.paramBinds.Length; i++)
            {
                //paramBinds存的是变量的名称
                string text = this.paramBinds[i];
                if (!string.IsNullOrEmpty(text))
                {//拿到所有的变量
                    UIVariable uIVariable = base.FindVariable(text);
                    if (uIVariable == null)
                    {
                        Debug.LogError(name + " can not find a variable " + text);
                    }
                    else
                    {
                        //将刷新text的方法 挂载到 变量初始化的的回调上
                        uIVariable.OnValueInitialized += new Action(SetFormat);
                        //将刷新text的方法 挂载到 变量改变的回调上
                        uIVariable.OnValueChanged += new Action(SetFormat);
                        uIVariable.AddBind(this);
                        //给当前的variableList变量列表赋值
                        variableList[i] = uIVariable;
                    }
                }
            }
            SetFormat();
        }
    }

    /// <summary>
    /// 解绑变量  销毁时调用
    /// </summary>
    protected override void UnbindVariables()
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
