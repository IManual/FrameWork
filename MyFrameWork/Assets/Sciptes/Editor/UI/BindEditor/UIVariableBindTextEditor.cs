using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(UIVariableBindText))]
class UIVariableBindTextEditor : Editor
{
    ReorderableList list;
    UIVariableBindText self;
    SerializedProperty paramProperty;
    SerializedProperty variableTable;
    SerializedProperty element;

    int[] intList = new int[10];
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
        self = (UIVariableBindText)target;
        variableTable = serializedObject.FindProperty("variableTable");
        paramProperty = serializedObject.FindProperty("paramBinds");
        list = new ReorderableList(serializedObject, paramProperty, true, true, true, true);
        list.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "ParamsList:");
        };
        if (self.VariableTable == null) self.GetTable();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        options.Clear();
        variableTable.objectReferenceValue = EditorGUILayout.ObjectField("Variable Table", self.VariableTable, typeof(UIVariableTable), true) as UIVariableTable;
        self.Format = EditorGUILayout.TextField("Format", self.Format);

        if (self.VariableTable != null)
        {
            if (self.VariableTable.Variables != null)
            {
                for (int i = 0; i < self.VariableTable.Variables.Length; i++)
                {
                    if (self.VariableTable.Variables != null)
                    {
                        if (!string.IsNullOrEmpty(self.VariableTable.Variables[i].Name))
                        {
                            if (self.VariableTable.Variables[i].Type == UIVariableType.String
                                || self.VariableTable.Variables[i].Type == UIVariableType.Boolean
                                || self.VariableTable.Variables[i].Type == UIVariableType.Interger
                                || self.VariableTable.Variables[i].Type == UIVariableType.Float
                                )
                            {
                                options.Add(self.VariableTable.Variables[i].Name);
                            }
                        }
                    }
                }
            }
            options.Add("null");
        }

        list.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            element = paramProperty.GetArrayElementAtIndex(index);
            if (!string.IsNullOrEmpty(element.stringValue))
            {
                for (int i = 0; i < options.Count; i++)
                {
                    if (options[i] == element.stringValue)
                    {
                        intList[index] = i;
                    }
                }
            }
            intList[index] = EditorGUI.Popup(rect, "Element " + index, intList[index], options.ToArray());
            try
            {
                if (options[intList[index]] == "null")
                    element.stringValue = default(string);
                else
                    element.stringValue = options[intList[index]];
            }
            catch (Exception)
            {
                intList[index] = 0;
                element.stringValue = options[0];
            }
        }; 

        //刷新
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}