using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(UINameTable))]

public class NameTableEditor : Editor {

    ReorderableList list;
    SerializedProperty element;

    private void OnEnable()
    {
        var prop = serializedObject.FindProperty("component");
        list = new ReorderableList(serializedObject, prop);
        list.drawElementCallback = 
            (rect, index, isActive, isFocused) =>
        {
            element = prop.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element);
        };
        list.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "Name", "Widget");
        };
    }

    public override void OnInspectorGUI()
    {
        //拉取最新数据
        serializedObject.Update();
        //绘制list
        list.DoLayoutList();
        //应用修改
        serializedObject.ApplyModifiedProperties();
    }

}
