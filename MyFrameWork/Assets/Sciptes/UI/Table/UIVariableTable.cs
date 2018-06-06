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
    public UIVariable[] variables;

    //变量名字与变量的字典
    public Dictionary<string, UIVariable> variableDic;

    [CompilerGenerated]
    private static Comparison<UIVariable> compairson;

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

    //将变量集合排序
    public void Sort()
    {
        UIVariable[] arg_23_0 = this.variables;
        if (UIVariableTable.compairson == null)
        {
            UIVariableTable.compairson = new Comparison<UIVariable>(UIVariableTable.Compairson);
        }
        Array.Sort<UIVariable>(arg_23_0, UIVariableTable.compairson);
    }

    //初始化绑定
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
            UIVariableTable.ActivateChildBind(transform2);
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
            UIVariableTable.ActivateChildBind(transform2);
        }
    }

    protected override void Awake()
    {
        UIVariableTable.ActivateSelfBind(transform);
    }

    //比较依据
    private static int Compairson(UIVariable uIVariable, UIVariable uIVariable2)
    {
        return uIVariable.Name.CompareTo(uIVariable2.Name);
    }
}
