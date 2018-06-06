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
    string[] names = new string[] { "null" };
    SerializedProperty paramProperty;
    int[] intList = new int[10];
    List<String> paramList = new List<string>();

    private void OnEnable()
    {
        for (int i = 0; i < intList.Length; i++)
        {
            intList[i] = 0;
        }
        self = (UIVariableBindText)target;
        paramProperty = serializedObject.FindProperty("paramBinds");
        list = new ReorderableList(serializedObject, paramProperty, true, true, true, true);
        list.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "ParamsList:");
        };
    }

    public override void OnInspectorGUI()
    {
        paramList.Clear();
        EditorGUILayout.ObjectField("Variable Table", self.VariableTable, typeof(UIVariableTable), true);
        self.Format = EditorGUILayout.TextField("Format", self.Format);

        if (self.variableTable != null)
        {
            for (int i = 0; i < self.variableTable.variables.Length; i++)
            {
                if (!string.IsNullOrEmpty(self.variableTable.variables[i].Name))
                {
                    if (self.variableTable.variables[i].Type == UIVariableType.String
                        || self.variableTable.variables[i].Type == UIVariableType.Boolean
                        || self.variableTable.variables[i].Type == UIVariableType.Interger
                        || self.variableTable.variables[i].Type == UIVariableType.Float
                        )
                    {
                        paramList.Add(self.variableTable.variables[i].Name);
                    }
                }
            }
            paramList.Insert(0, "null");
            names = paramList.ToArray();
        }

        list.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            intList[index] = EditorGUI.Popup(rect, "Element " + index, intList[index], names);
            try
            {
                self.ParamBinds[index] = names[intList[index]];
            }
            catch (Exception)
            {
                intList[index] = 0;
                self.ParamBinds[index] = names[0];
            }
        }; 

        //刷新
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
