using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

/// <summary>
/// 绘制VariableBindActive脚本
/// </summary>
[CustomEditor(typeof(UIVariableBindActive))]
public class UIVariableBindActiveEditor : Editor
{
    UIVariableBindActive self;
    ReorderableList list;
    SerializedProperty variables;
    SerializedProperty booleanLogic;
    SerializedProperty transitionMode;
    SerializedProperty transitionTime;
    int[] intList = new int[10];

    Rect line_first;
    Rect line_sercond;

    SerializedProperty variableTable;
    SerializedProperty currrent;
    SerializedProperty variableName;
    SerializedProperty reverse;

    List<String> options = new List<string>();

    private void Awake()
    {
        for (int i = 0; i < intList.Length; i++)
        {
            intList[i] = 0;
        }
    }

    private void OnEnable()
    {
        self = (UIVariableBindActive)target;
        variableTable = serializedObject.FindProperty("variableTable");

        variables = serializedObject.FindProperty("variables");
        booleanLogic = serializedObject.FindProperty("booleanLogic");
        transitionMode = serializedObject.FindProperty("transitionMode");
        transitionTime = serializedObject.FindProperty("transitionTime");

        list = new ReorderableList(serializedObject, variables, true, true, true, true);
        list.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "Variables:");
        };

        list.elementHeight = EditorGUIUtility.singleLineHeight * 2;
        list.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            currrent = variables.GetArrayElementAtIndex(index);
            variableName = currrent.FindPropertyRelative("variableName");
            reverse = currrent.FindPropertyRelative("reverse");
            Debug.Log(variableName.stringValue);
            if (!string.IsNullOrEmpty(variableName.stringValue))
            {
                for (int i = 0; i < options.Count; i++)
                {
                    if (options[i] == variableName.stringValue)
                    {
                        intList[index] = i;
                    }
                }
            }
            line_first = new Rect(
                rect.x,
                rect.y,
                rect.width,
                EditorGUIUtility.singleLineHeight);
            line_sercond = new Rect(
                  rect.x,
                rect.y + EditorGUIUtility.singleLineHeight,
                rect.width,
                EditorGUIUtility.singleLineHeight);

            intList[index] = EditorGUI.Popup(line_first, "Variable Name ", intList[index], options.ToArray());
            reverse.boolValue = EditorGUI.Toggle(line_sercond, "Reverse", reverse.boolValue);
            try
            {
                if (options[intList[index]] == "null")
                    variableName.stringValue = default(string);
                else
                    variableName.stringValue = options[intList[index]];
            }
            catch (Exception) { }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();      //刷新最新的数据
        variableTable.objectReferenceValue = EditorGUILayout.ObjectField("Variable Table", self.VariableTable, typeof(UIVariableTable), true) as UIVariableTable;
        booleanLogic.enumValueIndex = 
            (int)(UIVariableBindBool.BooleanLogic)EditorGUILayout.EnumPopup("Boolean logic", (UIVariableBindBool.BooleanLogic)booleanLogic.enumValueIndex);

        //绘制列表
        options.Clear();
        if (self.VariableTable != null)
        {
            for (int i = 0; i < self.VariableTable.Variables.Length; i++)
            {
                if (self.VariableTable.Variables != null)
                {
                    if (!string.IsNullOrEmpty(self.VariableTable.Variables[i].Name))
                    {
                        if (self.VariableTable.Variables[i].Type == UIVariableType.Boolean
                            || self.VariableTable.Variables[i].Type == UIVariableType.Interger
                            || self.VariableTable.Variables[i].Type == UIVariableType.Float
                            )
                        {
                            options.Add(self.VariableTable.Variables[i].Name);
                        }
                    }
                }
            }
            options.Add("null");
        }

        list.DoLayoutList();
        transitionTime.floatValue = EditorGUILayout.FloatField("Transition Time", transitionTime.floatValue);
        transitionMode.enumValueIndex = 
            (int)(UIVariableBindActive.TransitionModeEnum)EditorGUILayout.EnumPopup("Transition Mode", (UIVariableBindActive.TransitionModeEnum)transitionMode.enumValueIndex);
        serializedObject.ApplyModifiedProperties(); //应用修改的数据
    }
}
