using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// UI变量配置表
/// </summary>
[AddComponentMenu("UI/Bind/UI Variable Table")]
public class UIVariableTable : BaseBehaviour {

    // 变量参数数组           通过editor在编辑器下赋值
    [SerializeField]
    private UIVariable[] variables;

    //变量名字与变量的字典
    public Dictionary<string, UIVariable> variableDic;

    public UIVariable[] Variables
    {
        get
        {
            return this.variables;
        }
    }

    //将变量存储
    private Dictionary<string, UIVariable> GetVariableDic()
    {
        if (this.variableDic == null)
        {
            //字典根据键进行排序
            this.variableDic = new Dictionary<string, UIVariable>(StringComparer.Ordinal);
            if (this.variables != null)
            {
                UIVariable[] array = this.variables;
                for (int i = 0; i < array.Length; i++)
                {
                    UIVariable uIVariable = array[i];
                    this.variableDic.Add(uIVariable.Name, uIVariable);
                }
            }
        }
        return this.variableDic;
    }

    /// <summary>
    /// 根据变量名字找到对应变量
    /// </summary>
    public UIVariable FindVariable(string name)
    {
        UIVariable result;
        if (this.GetVariableDic().TryGetValue(name, out result))
        {
            return result;
        }
        return null;
    }

    /// <summary>
    /// 添加变量
    /// </summary>
    public void AddDefaultVariable()
    {
        UIVariable uIVariable = new UIVariable();
        if (this.variables == null)
        {
            this.variables = new UIVariable[1];
            this.variables[0] = uIVariable;
        }
        else
        {   //将元素添加至数组最后一位
            ArrayUtility.Add<UIVariable>(ref this.variables, uIVariable);
        }
    }

    public string[] GetVariableNames()
    {
        string[] array = new string[this.variables.Length];
        for (int i = 0; i < this.variables.Length; i++)
        {
            array[i] = this.variables[i].Name;
        }
        return array;
    }

    //初始化绑定  游戏运行时调用
    public void InitializeBinds()
    {
        UIVariableTable.ActivateSelfBind(base.transform);
    }

    public UIVariable GetVariable(int index)
    {
        return this.variables[index];
    }

    //激活自身bind
    private static void ActivateSelfBind(Transform transform)
    {
        //查找自身所有继承自UIVariableBind的实现类 将他们初始化
        UIVariableBind[] variableBinds = transform.GetComponents<UIVariableBind>();
        for (int i = 0; i < variableBinds.Length; i++)
        {
            variableBinds[i].Init();
        }
        //遍历自身变换组件 可以遍历到所有子物体 (不包含孙子物体以及自身)
        foreach (Transform transform2 in transform)
        {
            ActivateChildBind(transform2);
        }
    }

    //激活子类bind
    private static void ActivateChildBind(Transform transform)
    {
        //如果子物体身上也存在UIVariableTable 则不进行操作
        if (transform.HasComponent<UIVariableTable>())
        {
            return;
        }
        UIVariableBind[] variableBinds = transform.GetComponents<UIVariableBind>();
        for (int i = 0; i < variableBinds.Length; i++)
        {
            variableBinds[i].Init();
        }
        //遍历自身变换组件 可以遍历到所有子物体 (不包含孙子物体以及自身)
        foreach (Transform transform2 in transform)
        {
            ActivateChildBind(transform2);
        }
    }

    protected override void Awake()
    {
        ActivateSelfBind(transform);
    }

    protected override void OnValidate()
    {
        variableDic = null;
        if (variables != null)
        {
            UIVariable[] array = this.variables;
            for (int i = 0; i < array.Length; i++)
            {            
                UIVariable uIVariable = array[i];
                uIVariable.AddDefaultValue();
                uIVariable.ClearBindList();
                uIVariable.ValueChange();
            }        
        }
        GetRepeatVariable();
    }

    public List<string> repeatVar = new List<string>();
    /// <summary>
    /// Editor脚本绘制时获取重复变量
    /// </summary>
    public List<string> GetRepeatVariable()
    {
        repeatVar.Clear();
        List<string> tmp = new List<string>();
        UIVariable[] array = this.variables;
        foreach (var item in array)
        {
            if (tmp.Contains(item.Name)) { repeatVar.Add(item.Name); }
            else { tmp.Add(item.Name); }
        }
        return repeatVar;
    }
}
