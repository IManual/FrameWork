using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIEventBindClick))]
public class UIEventBindClickEditor : Editor
{
    UIEventBindClick self;
    SerializedProperty eventTable;
    SerializedProperty eventName;
    int index;
    private void OnEnable()
    {
        self = (UIEventBindClick)target;
        eventTable = serializedObject.FindProperty("eventTable");
        eventName = serializedObject.FindProperty("eventName");
    }
    List<string> options = new List<string>();

    public override void OnInspectorGUI()
    {
        options.Clear();
        serializedObject.Update();
        eventTable.objectReferenceValue = EditorGUILayout.ObjectField("Event Table", self.EventTable, typeof(UIEventTable), true);
        if (self.EventTable != null)
        {
            options.AddRange(self.EventTable.Events);
        }
        options.Add("null");
        index = EditorGUILayout.Popup("Bind Event", index, options.ToArray());
        eventName.stringValue = options[index];
        serializedObject.ApplyModifiedProperties();
    }
}
