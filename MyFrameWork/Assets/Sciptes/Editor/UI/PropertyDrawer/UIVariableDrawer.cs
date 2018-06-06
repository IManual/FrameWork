using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 绘制单个variable对象
/// </summary>

[CustomPropertyDrawer(typeof(UIVariable))]
public class UIVariableDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Find properties.
        SerializedProperty nameProperty = property.FindPropertyRelative("name");
        SerializedProperty gameProperty = property.FindPropertyRelative("type");
        var rectLine = new Rect(
            position.x,
            position.y,
            position.width,
            EditorGUIUtility.singleLineHeight);
        var nameRect = new Rect(rectLine)
        {
            width = rectLine.width * 0.5f,
        };
        var gameRect = new Rect(rectLine)
        {
            width = rectLine.width * 0.5f,
            x = rectLine.width * 0.5f + 35f,
        };
        //绘制上一行
        nameProperty.stringValue = EditorGUI.TextField(nameRect, nameProperty.stringValue);
        gameProperty.intValue = (int)(UIVariableType)EditorGUI.EnumPopup(gameRect, (UIVariableType)gameProperty.enumValueIndex);
        rectLine.y += EditorGUIUtility.singleLineHeight;
        //绘制下一行
        if (gameProperty.enumValueIndex == 0)
        {
            DrawBoolean(rectLine, property, label);
        }
        if (gameProperty.enumValueIndex == 1)
        {
            DrawInterge(rectLine, property, label);
        }
        if (gameProperty.enumValueIndex == 2)
        {
            DrawFloat(rectLine, property, label);
        }
        if (gameProperty.enumValueIndex == 3)
        {
            DrawString(rectLine, property, label);
        }
        if (gameProperty.enumValueIndex == 4)
        {
            DrawAsset(rectLine, property, label);
        }
    }

    /// <summary>
    /// bool类型绘制
    /// </summary>
    /// <param name="nameProperty"></param>
    /// <param name="gameProperty"></param>
    /// <param name="position"></param>
    /// <param name="label"></param>
    /// <param name="property"></param>
    public void DrawBoolean(Rect rectLine, SerializedProperty property, GUIContent label)
    {
        var booleanProperty = property.FindPropertyRelative("booleanValue");
        booleanProperty.boolValue = EditorGUI.Toggle(rectLine, booleanProperty.boolValue);
        EditorGUI.EndProperty();
    }

    public void DrawFloat(Rect rectLine, SerializedProperty property, GUIContent label)
    {
        var floatProperty = property.FindPropertyRelative("floatValue");
        floatProperty.floatValue = EditorGUI.FloatField(rectLine, floatProperty.floatValue);
        EditorGUI.EndProperty();
    }

    public void DrawString(Rect rectLine, SerializedProperty property, GUIContent label)
    {
        var stringProperty = property.FindPropertyRelative("stringValue");
        stringProperty.stringValue = EditorGUI.TextField(rectLine, stringProperty.stringValue);
        EditorGUI.EndProperty();
    }

    public void DrawInterge(Rect rectLine, SerializedProperty property, GUIContent label)
    {
        var intergerProperty = property.FindPropertyRelative("intergerValue");
        intergerProperty.longValue = EditorGUI.LongField(rectLine, intergerProperty.longValue);
        EditorGUI.EndProperty();
    }

    string bundleName = "";
    string assetName = "";
    AssetID assetID;
    public void DrawAsset(Rect rectLine, SerializedProperty property, GUIContent label)
    {
        var assetProperty = property.FindPropertyRelative("assetValue");

        var bundleNameRect = new Rect(rectLine)
        {
            width = rectLine.width * 0.5f,
        };
        var assetNameRect = new Rect(rectLine)
        {
            width = rectLine.width * 0.5f,
            x = rectLine.width * 0.5f + 35f,
        };
        //绘制上一行
        bundleName = EditorGUI.TextField(new Rect(rectLine) { width = rectLine.width * 0.5f, }, bundleName);
        assetName = EditorGUI.TextField(new Rect(rectLine) { width = rectLine.width * 0.5f, x = rectLine.width * 0.5f + 35f, }, assetName);
        assetID = new AssetID(bundleName, assetName);     
        EditorGUI.EndProperty();
    }
}
