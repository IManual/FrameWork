using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 绘制VariableBindActive脚本
/// </summary>
[CustomEditor(typeof(UIVariableBindActive))]
public class UIVariableBindActiveEditor : Editor {

    UIVariableBindActive bind;

    private void OnEnable()
    {//targer为自身引用
        bind = (UIVariableBindActive)target;
        //查找当前引用的VariableTable
        if (bind.variableTable == null)
        {
            bind.variableTable = Tool.FindFather(bind.transform, a =>
            {
                return a.GetComponent<UIVariableTable>() != null;
            }).GetComponent<UIVariableTable>();
        }
    }

    public override void OnInspectorGUI()
    {
        //UIVariableTable variableTable = bind.variableTable;
        ////更新数据
        //if (variableTable != null)
        //{ 
        //    List<String> nameByType = new List<String>();
        //    //拿到所有bool类型
        //    for (int i = 0; i < variableTable.variables.Length; i++)
        //    {
        //        if(variableTable.variables[i].type == UIVariableType.Bollean)
        //        {
        //            nameByType.Add(variableTable.variables[i].name);
        //        }
        //    }
        //    string[] names = new string[nameByType.Count];
        //    names = nameByType.ToArray();
        //    //将所有bool类型变量填充到界面 index（当前选择的变量索引)
        //    bind.index = EditorGUILayout.Popup("Boolean logic:", bind.index, names);
        //    //查找绑定的变量对应在VariableTable上的值
        //    if (name.Length > 0)
        //    {
        //        foreach (var item in variableTable.variables)
        //        {
        //            Debug.Log(item.name);
        //            Debug.Log(names[bind.index]);
        //            if (item.name == names[bind.index])
        //            {//实际绑定
        //                bind.variable = item;
        //            }
        //        }
        //    }          
        // }
        //刷新
        base.OnInspectorGUI();
    }
}
