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
public class UIVariableBindActiveEditor : Editor {

    UIVariableBindActive self;
    ReorderableList list;
    SerializedProperty variables;
    SerializedProperty booleanLogic;
    SerializedProperty transitionMode;
    SerializedProperty transitionTime;
    int[] intList = new int[10];

    Rect line_first;
    Rect line_sercond;
    SerializedProperty currrent;
    SerializedProperty variableName;
    SerializedProperty reverse;

    private void Awake()
    {
        for (int i = 0; i < intList.Length; i++)
        {
            intList[i] = 0;
        }
    }

    private void OnEnable()
    {//targer为自身引用
        self = (UIVariableBindActive)target;
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

            if (!string.IsNullOrEmpty(variableName.stringValue))
            {
                for (int i = 0; i < names.Length; i++)
                {
                    if (names[i] == variableName.stringValue)
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

            intList[index] = EditorGUI.Popup(line_first, "Variable Name ", intList[index], names);
            reverse.boolValue = EditorGUI.Toggle(line_sercond, "Reverse", reverse.boolValue);
            try
            {
                variableName.stringValue = names[intList[index]];
            }
            catch (Exception) { }
        };
    }

    List<String> paramList = new List<string>();
    string[] names = new string[] { };
    public override void OnInspectorGUI()
    {
        serializedObject.Update();      //刷新最新的数据
        self.variableTable = EditorGUILayout.ObjectField("Variable Table", self.variableTable, typeof(UIVariableTable), true) as UIVariableTable;
        booleanLogic.enumValueIndex = 
            (int)(UIVariableBindBool.BooleanLogic)EditorGUILayout.EnumPopup("Boolean logic", (UIVariableBindBool.BooleanLogic)booleanLogic.enumValueIndex);

        //绘制列表
        paramList.Clear();
        if (self.variableTable != null)
        {
            for (int i = 0; i < self.variableTable.Variables.Length; i++)
            {
                if (self.variableTable.Variables != null)
                {
                    if (!string.IsNullOrEmpty(self.variableTable.Variables[i].Name))
                    {
                        if (self.variableTable.Variables[i].Type == UIVariableType.String
                            || self.variableTable.Variables[i].Type == UIVariableType.Boolean
                            || self.variableTable.Variables[i].Type == UIVariableType.Interger
                            || self.variableTable.Variables[i].Type == UIVariableType.Float
                            )
                        {
                            paramList.Add(self.variableTable.Variables[i].Name);
                        }
                    }
                }
            }
            names = paramList.ToArray();
        }

        list.DoLayoutList();
        transitionTime.floatValue = EditorGUILayout.FloatField("Transition Time", transitionTime.floatValue);
        transitionMode.enumValueIndex = 
            (int)(UIVariableBindActive.TransitionModeEnum)EditorGUILayout.EnumPopup("Transition Mode", (UIVariableBindActive.TransitionModeEnum)transitionMode.enumValueIndex);
        serializedObject.ApplyModifiedProperties(); //应用修改的数据
        self.BindVariables();
    }
}
