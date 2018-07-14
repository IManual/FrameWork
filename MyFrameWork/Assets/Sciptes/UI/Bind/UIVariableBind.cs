using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    private UIVariableTable variableTable;

    /// <summary>
    /// 是否开始运行
    /// </summary>
    private bool start;

    //[CompilerGenerated]
    //private UIVariableTable table;

    /// <summary>
    /// 当前绑定的variableTable
    /// </summary>
    public UIVariableTable VariableTable
    {
        get;
        private set;
    }

    /// <summary>
    /// 游戏启动时 table进行一次调用 当前脚本挂载时也调用一次
    /// </summary>
    public void Init()
    {
        if (!start)
        {
            this.start = true;
            this.GetTable();
            this.BindVariables();
        }
    }

    /// <summary>
    /// table提供的根据变量名查找变量
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 挂载当前脚本的物体销毁时调用子类unbind
    /// </summary>
    protected override void OnDestroy()
    {
        this.UnbindVariables();
        this.start = false;
    }

    /// <summary>
    /// 子类重写 分别在界面创建打开时  销毁时调用
    /// </summary>
    protected abstract void BindVariables();

    protected abstract void UnbindVariables();

    /// <summary>
    /// 挂载后开始运行
    /// </summary>
    protected override void Awake()
    {
        this.Init();
    }

    protected override void OnValidate()
    {
        PrefabType prefabType = PrefabUtility.GetPrefabType(gameObject);      
        if (prefabType != PrefabType.Prefab)
        {
            UnbindVariables();
            start = true;
            GetTable();
            BindVariables();
        }
        else if (this.variableTable == null)
        {
            this.variableTable = this.GetComponentInParentHard<UIVariableTable>();
        }
    }

    /// <summary>
    /// 从自身开始向上查找table
    /// </summary>
    public void GetTable()
    {
        if (this.variableTable == null)
        {
            this.variableTable = this.GetComponentInParentHard<UIVariableTable>();
        }
        this.VariableTable = this.variableTable;
    }
}
