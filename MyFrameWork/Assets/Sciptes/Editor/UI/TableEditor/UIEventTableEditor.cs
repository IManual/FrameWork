using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(UIEventTable))]
public class UIEventTableEditor : Editor {

    ReorderableList list;
    SerializedProperty element;
    Rect eventRect;

    private void OnEnable()
    {
        var prop = serializedObject.FindProperty("events");
        list = new ReorderableList(serializedObject, prop);
        list.drawElementCallback = 
            (rect, index, isActive, isFocused) =>
        {
            eventRect = new Rect(rect)
            {
                width = rect.width,
                y = rect.y + 2,
                height = EditorGUIUtility.singleLineHeight,
            };
            element = prop.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(eventRect, element, GUIContent.none);
        };
        list.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "EventName");
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
