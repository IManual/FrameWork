using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(UIEvent))]
//public class UIEventDrawer : PropertyDrawer {
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        using (new EditorGUI.PropertyScope(position, label, property))
//        {
//            var eventnameProperty = property.FindPropertyRelative("eventName");

//            var eventnameRect = new Rect(position) { y = position.y + 3,height = EditorGUIUtility.singleLineHeight, };

//            eventnameProperty.stringValue = EditorGUI.TextField(eventnameRect, eventnameProperty.stringValue);
//        }
//    }
//}
