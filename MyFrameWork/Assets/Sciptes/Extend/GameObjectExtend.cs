using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public static class GameObjectExtend {

    public static T GetOrAddComponent<T>(this GameObject self) where T : MonoBehaviour
    {
        T comp = self.GetComponent<T>();
        if (comp == null)
        {
            comp = self.AddComponent<T>();
        }
        return comp;
    }

    public static Object GetOrAddComponent(this GameObject self, System.Type type)
    {
        var comp = self.GetComponent(type);
        if (comp == null)
        {
            comp = self.AddComponent(type);
        }
        return comp;
    }
}
