using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sort
{
    public static void QuickSort(int[] array, int left, int right)
    {
        int temp = array[left];
        int i = left;
        int j = right;
        //当左边小于右边时  一直进行比较
        while (i < j)
        {
            //标志位和右边比   找右边比当前小的
            while (temp <= array[j] && i < j) j--;
            //将这个大的先存下来 此时array[i] 已经存在temp中
            array[i] = array[j];
            //标志位和左边比   找左边比当前大的
            while (temp >= array[i] && i < j) i++;
            //将左右两个调换位置
            array[j] = array[i];
        }
        //完成一轮调换
        if (i - 1 > left) QuickSort(array, left, i - 1);
        if (i + 1 < right) QuickSort(array, i + 1, right);
    }

    //                  A
    //        B                    C
    //  D           E      F            G
    //先序：ABDECFG
    //
    public static void PreOder(Node node)
    {
        Stack stack = new Stack();
        Node node_pop;
        Node current = node;
        //如果当前节点不为空 或者栈中有元素
        while (current != null)
        { 
            //入栈
            stack.Push(current);
            current.Visit();
            current = current.leftChild;
            //如果当前节点没有左节点了 且栈中有元素 开始出栈
            while (current == null && stack.Count > 0)
            {
                node_pop = stack.Pop() as Node;
                //如果当前节点的右节点不为空 跳回第一层 先输出 继续查找左节点
                current = node_pop.rightChild;
            }
        }
    }

    //                  A
    //        B                    C
    //  D           E      F            G
    //中序：DBEAFCG
    //
    public static void MidOder(Node node)
    {
        Stack stack = new Stack();
        Node node_pop;
        Node current = node;
        //如果当前节点不为空 或者栈中有元素
        while (current != null)
        {
            //入栈
            stack.Push(current);
            current = current.leftChild;
            //如果当前节点没有左节点了 且栈中有元素 开始出栈
            while (current == null && stack.Count > 0)
            {
                node_pop = stack.Pop() as Node;
                node_pop.Visit();
                //如果当前节点的右节点不为空 跳回第一层 先输出 继续查找左节点
                current = node_pop.rightChild;
            }
        }
    }

    //                  A
    //        B                    C
    //  D           E      F            G
    //     H
    //后序：HDEBFGCA
    //
    public static void LaterOder(Node node)
    {
        Dictionary<string, int> times = new Dictionary<string, int>();
        Stack stack = new Stack();
        Node node_pop;
        Node current = node;
        //如果当前节点不为空 或者栈中有元素
        while (current != null)
        {
            if (times.ContainsKey(current.name))
            {
                int value;
                times.TryGetValue(current.name, out value);
                times[current.name] = value + 1;
            }
            else
            {
                times[current.name] = 1;
            }
            //入栈
            stack.Push(current);
            current = current.leftChild;
            //如果当前节点没有左节点了 且栈中有元素 开始出栈
            while (current == null && stack.Count > 0)
            {
                node_pop = stack.Pop() as Node;
                if (times.ContainsKey(node_pop.name))
                {
                    int value;
                    times.TryGetValue(node_pop.name, out value);
                    times[node_pop.name] = value + 1;
                }
                else
                {
                    times[node_pop.name] = 1;
                }
                if (times[node_pop.name] == 3)
                {
                    node_pop.Visit();
                    node_pop = stack.Pop() as Node;
                    node_pop = stack.Pop() as Node;
                }
                //如果当前节点的右节点不为空 跳回第一层 先输出 继续查找左节点
                current = node_pop.rightChild;
                if (current == null)
                {
                    node_pop.Visit();
                }
                else
                {
                    stack.Push(node_pop);
                }
            }
        }
    }
}

public class Node
{
    public string name;

    public Node(string name)
    {
        this.name = name;
    }
    public Node leftChild;

    public Node rightChild;

    public void Visit()
    {
        Debug.Log(name);
    }
}
