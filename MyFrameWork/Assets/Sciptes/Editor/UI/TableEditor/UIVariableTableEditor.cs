using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(UIVariableTable))]
public class UIVariableTableEditor : Editor
{
    ReorderableList list;

    private void OnEnable()
    {
        //当前table对象
        var table = (UIVariableTable)target;

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
