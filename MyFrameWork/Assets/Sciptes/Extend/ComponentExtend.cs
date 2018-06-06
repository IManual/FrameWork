using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentExtend {

    public static T GetComponentInParentHard<T>(this Component self)
    {
        T component = default(T);
        component = self.GetComponent<T>();
        if(component == null)
        {
            component = self.GetComponentInParent<T>();
        }
        return component;
    }

    public static bool HasComponent<T>(this Component self)
    {
        return self.GetComponent<T>() == null;
    }
}
