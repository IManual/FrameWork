using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtend
{
    public static void Shuffle<T>(this IList<T> array)
    {
        for (int i = 0; i < array.Count; i++)
        {
            int index = Random.Range(0, array.Count);
            T value = array[index];
            array[index] = array[i];
            array[i] = value;
        }
    }

    public static void RemoveDuplicate<T>(this List<T> list)
    {
        Dictionary<T, int> dictionary = new Dictionary<T, int>();
        foreach (T current in list)
        {
            int num = 0;
            if (!dictionary.TryGetValue(current, out num))
            {
                dictionary.Add(current, 0);
            }
        }
        list.Clear();
        foreach (KeyValuePair<T, int> current2 in dictionary)
        {
            list.Add(current2.Key);
        }
    }
}
