using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(CircleImage), true)]
[CanEditMultipleObjects]
public class CicleImageEditor : ImageEditor
{
    private SerializedProperty segmentCount;
    private SerializedProperty fillPercent;

    protected override void OnEnable()
    {
        base.OnEnable();
        segmentCount = serializedObject.FindProperty("segmentCount");
        fillPercent = serializedObject.FindProperty("fillPercent");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        serializedObject.Update();
        EditorGUILayout.PropertyField(segmentCount);
        EditorGUILayout.PropertyField(fillPercent);
        serializedObject.ApplyModifiedProperties();
    }
}
