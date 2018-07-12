﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(UIVariableTable))]
public class UIVariableTableEditor : Editor
{
    ReorderableList list;
    UIVariableTable table;

    private void OnEnable()
    {
        //当前table对象
        table = (UIVariableTable)target;

        var prop = serializedObject.FindProperty("variables");

        list = new ReorderableList(serializedObject, prop, true, true, true, true);
        list.elementHeight = EditorGUIUtility.singleLineHeight * 2 + 4;
        list.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = prop.GetArrayElementAtIndex(index);
            rect.y += 2;
            rect.height = EditorGUIUtility.singleLineHeight * 3;
            EditorGUI.PropertyField(rect, element);//绘制一个variable 输入的值传递给variable
        };

    }

    List<int> repeatList = new List<int>();
    public override void OnInspectorGUI()
    {
        serializedObject.Update();      //更新对象最新数据
        table.FlushVariableDic();
        repeatList.Clear();
        //标记重复的变量
        if (table.Variables != null)
        {
            for (int i = 0; i < table.Variables.Length; i++)
            {
                if (table.GetRepeatVariable().Contains(table.Variables[i].Name))
                {
                    repeatList.Add(i);
                }
            }
            list.drawElementBackgroundCallback = (rect, index, isActive, isFocused) =>
            {
                if (repeatList.Contains(index)) GUI.backgroundColor = Color.red;
                else
                {
                    if (isFocused)
                    {
                        GUI.backgroundColor = Color.gray;
                    }
                    else
                    {
                        GUI.backgroundColor = Color.white;
                        //修改table时同步触发各个Variable绑定的物体的刷新
                        try { 
                            foreach (var item in table.Variables[index].variableBindList)
                            {
                                item.BindVariables();
                                EditorUtility.SetDirty(item);
                            }
                        }catch (Exception e) {};
                    }
                }
            };

            list.drawHeaderCallback = (rect) =>
            {
                EditorGUI.LabelField(rect, "Variables:");
            };
        }
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
