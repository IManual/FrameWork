using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

public class RadialLayoutGroupEditor : HorizontalOrVerticalLayoutGroupEditor
{
    private SerializedProperty distance;
    private SerializedProperty minAngle;
    private SerializedProperty maxAngle;
    private SerializedProperty startAngle;

    protected override void OnEnable()
    {
        //distance = serializedObject.FindProperty("distance");
        //minAngle = serializedObject.FindProperty("minAngle");
        //maxAngle = serializedObject.FindProperty("maxAngle");
        //startAngle = serializedObject.FindProperty("startAngle");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        //serializedObject.Update();
        //serializedObject.ApplyModifiedProperties();
    }
}
