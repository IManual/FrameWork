using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Tool
{
    /// <summary>
    /// 寻找父物体的Transform组件
    /// </summary>
    public static Transform FindFather(Transform target, Func<Transform,bool> func)
    {
        if (func(target) || target.parent == null)
        {
            return target;
        }
        if (func(target.parent))
        {
            return target.parent;
        }
        else
        {
            target = FindFather(target.parent, func);
        }
        return target;
    }

    /// <summary>
    /// 获取字典中的值
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="dic"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T2 GetDicValueByKey<T1, T2>(Dictionary<T1, T2> dic, T1 key)
    {
        T2 value;
        return dic.TryGetValue(key, out value) ? value : default(T2);
    }
}

