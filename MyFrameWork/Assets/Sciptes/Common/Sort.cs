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

    public class Node
    {
        public Node leftChild;

        public Node rightChild;

        public void Visit()
        {

        }
    }

    public static void PreOder(Node node)
    {
        Stack stack = new Stack();
        Node node_pop;
        Node current = node;
        //如果当前节点不为空 或者栈中有元素
        while (current != null || stack.Count > 0)
        { 
            //入栈
            stack.Push(current);

            current = current.leftChild;
            //如果左孩子不为空 且 栈不为空
            while (current!= null && stack.Count > 0)
            {     
                node_pop = stack.Pop() as Node;
                //同时设置其右孩子为当前节点 循环判断 直到current为空
                current = current.rightChild;
            }
        }
    }
}
