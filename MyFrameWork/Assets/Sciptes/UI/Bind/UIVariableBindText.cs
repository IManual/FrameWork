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
    /// 绑定的变量名（string名称）列表
    /// </summary>
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
        if (this.UIText == null)
        {
            this.UIText = base.GetComponent<Text>();
        }
        if (UIText == null || paramBinds == null || variableList == null)
        {
            return;
        }
        //如果当前format是空的  只显示参数列表第一个参数的值
        if (string.IsNullOrEmpty(this.format))
        {//且参数名列表不为空
            if (this.paramBinds.Length > 0)
            {//拿到当前变量列表第一个
                UIVariable uIVariable = this.variableList[0];
                if (uIVariable != null)
                {//如果不为空 拿到当前变量的 值
                    object valueObject = uIVariable.ValueObject;
                    if (valueObject != null)
                    {   //如果有值
                        this.UIText.text = valueObject.ToString();
                    }
                }
                else
                {
                    this.UIText.text = "";
                }
            }
        }
        else
        {   //如果format 不为空
            object[] array = new object[this.paramBinds.Length];
            for (int i = 0; i < this.paramBinds.Length; i++)
            {//拿到所有变量
                UIVariable uIVariable2 = this.variableList[i];
                if (uIVariable2 != null)
                {//拿到所有变量的值
                    array[i] = uIVariable2.ValueObject;
                }
            }
            try
            {//尝试按照format格式进行组合
                tmp = this.format;
                if (Regex.Matches(tmp, "{").Count % 2 == 1 && Regex.Matches(tmp, "{").Count != Regex.Matches(tmp, "}").Count)
                {
                    tmp = tmp.Replace("{", "{{");                 
                }
                this.UIText.text = string.Format(tmp, array);
            }
            catch (FormatException ex)
            {//如果失败 抛出异常
                if (Application.isPlaying)
                {
                    Debug.Log(ex.Message, this);
                }
            }
        }
    }
    string tmp = "";
    /// <summary>
    /// 当前脚本挂载时调用一次  父类Init方法可调用当前方法
    /// </summary>
    public override void BindVariables()
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
                        //Debug.Log(name + "can not find a variable " + text);
                    }
                    else
                    {
                        //将刷新text的方法 挂载到 变量初始化的的回调上
                        uIVariable.OnValueInitialized += new Action(this.SetFormat);
                        //将刷新text的方法 挂载到 变量改变的回调上
                        uIVariable.OnValueChanged += new Action(this.SetFormat);
                        uIVariable.AddBind(this);
                        //给当前的variableList变量列表赋值
                        this.variableList[i] = uIVariable;
                    }
                }
            }
            //刷新一次text
            this.SetFormat();
        }
    }

    /// <summary>
    /// 解绑变量  销毁时调用
    /// </summary>
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
