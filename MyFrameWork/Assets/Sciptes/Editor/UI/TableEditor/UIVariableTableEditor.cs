using System;
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
    SerializedProperty element;

    private void OnEnable()
    {
        //当前table对象
        table = (UIVariableTable)target;

        var prop = serializedObject.FindProperty("variables");

        list = new ReorderableList(serializedObject, prop, true, true, true, true);
        list.elementHeight = EditorGUIUtility.singleLineHeight * 2 + 4;
        list.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            element = prop.GetArrayElementAtIndex(index);
            rect.y += 2;
            rect.height = EditorGUIUtility.singleLineHeight * 3;
            EditorGUI.PropertyField(rect, element);//绘制一个variable 输入的值传递给variable
        };
        list.drawElementBackgroundCallback = (rect, index, isActive, isFocused) =>
        {
            if (table.repeatVar.Contains(prop.GetArrayElementAtIndex(index).FindPropertyRelative("name").stringValue)) GUI.backgroundColor = Color.red;
            else
            {
                if (isFocused)
                {
                    GUI.backgroundColor = Color.gray;
                }
                else
                {
                    GUI.backgroundColor = Color.white;
                }
            }
        };

        list.onReorderCallbackWithDetails = (list, oldIndex, newIndex) =>
        {
                  
        };

        list.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "Variables:");
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();      //更新对象最新数据
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
