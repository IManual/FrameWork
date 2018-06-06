using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI变量绑定类的基类
/// </summary>
[ExecuteInEditMode]//使普通脚本在编辑器模式下运行
public abstract class UIVariableBind : BaseBehaviour {

    /// <summary>
    /// UI变量配置表
    /// </summary>
    [SerializeField, Tooltip("The variable table for this bind.")]
    public UIVariableTable variableTable;

    private bool start;

    [CompilerGenerated]
    private UIVariableTable table;

    public UIVariableTable VariableTable
    {
        get;
        private set;
    }

    internal void Init()
    {
        if (!start)
        {
            this.start = true;
            this.GetTable();
            this.BindVariables();
        }
    }

    public UIVariable FindVariable(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }
        if(this.VariableTable != null)
        {
            return this.VariableTable.FindVariable(name);
        }
        return null;
    }

    protected override void OnDestroy()
    {
        this.UnbindVariables();
        this.start = false;
    }

    public abstract void BindVariables();

    public abstract void UnbindVariables();

    //挂载后开始运行
    protected override void Awake()
    {
        this.Init();
    }

    /// <summary>
    /// 从自身开始向上查找table
    /// </summary>
    private void GetTable()
    {
        if (this.variableTable == null)
        {
            this.variableTable = this.GetComponentInParentHard<UIVariableTable>();
        }
        this.VariableTable = this.variableTable;
    }
}
