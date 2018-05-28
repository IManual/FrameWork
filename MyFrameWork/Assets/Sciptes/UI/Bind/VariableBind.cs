using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI变量绑定类
/// 这个脚本负责监听指定变量的改变事件
/// </summary>
public class VariableBind : BaseBehaviour {

    /// <summary>
    /// UI变量配置表
    /// </summary>
    public VariableTable table;

    /// <summary>
    /// UI变量参数
    /// </summary>
    [HideInInspector]
    public UIVariable variable;

    [HideInInspector]
    public int index;
}
