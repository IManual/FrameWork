using System;
using System.Collections.Generic;

public static class ArrayHelper
{   
    /// <summary>
    /// 筛选对象数组
    /// </summary>
    /// <typeparam name="T">对象数组的元素类型</typeparam>
    /// <typeparam name="TKey">筛选目标的类型</typeparam>
    /// <param name="array">对象数组</param>
    /// <param name="handler">筛选策略</param>
    /// <returns></returns>
    public static TKey[] Select<T, TKey>(T[] array, Func<T, TKey> handler)
    {
        TKey[] result = new TKey[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = handler(array[i]);
        }
        return result;
    } 

    /// <summary>
    /// 根据条件查找所有物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static T[] FindAll<T>(T[] array,Func<T,bool> condition)
    {
        List<T> result = new List<T>(array.Length);
        for (int i = 0; i < array.Length; i++)
        {
            if (condition(array[i])) result.Add(array[i]);
        }
        return result.ToArray();
    }

    /// <summary>
    /// 获取最小元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="array"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static T GetMin<T, TKey>(T[] array,Func<T,TKey> condition) where TKey:IComparable
    {
        T min = array[0];
        for (int i = 0; i < array.Length; i++)
        {
            if (condition(min).CompareTo(condition(array[i])) > 0)
                min = array[i];
        }
        return min;
    }
}
