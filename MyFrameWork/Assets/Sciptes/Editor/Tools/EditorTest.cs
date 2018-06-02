using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorTest
{
    [MenuItem("自定义工具/EditorTest")]
    private static void CreateHelloworldGameObject()
    {
        bool res = EditorUtility.DisplayDialog("Hello World", "确定创建对象？", "创建", "取消");
        if (res)
        {
            new GameObject("HellwoWorld");
        }
        Debug.Log(res);
    }
}
