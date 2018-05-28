using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public static class TransformExtend
{
    public static void SetLocalScale(this Transform self, float x, float y, float z)
    {
        self.localScale = new Vector3(x, y, z);
    }

    public static Transform FindHard(this Transform self, string path)
    {
        Transform tf = self.Find(path);
        if (tf == null)
        {
            for (int i = 0; i < self.childCount; i++)
            {
                tf = self.GetChild(i).FindHard(path);
                if (tf != null) return tf;
            } 
        }
        else
        {
            return tf;
        }
        return null;
    }
}
