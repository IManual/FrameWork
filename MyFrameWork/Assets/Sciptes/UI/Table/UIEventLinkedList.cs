using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 单个事件列表执行器
/// </summary>
public class UIEventLinkedList
{
    private LinkedList<SignalDelegate> linkedList;

    /// <summary>
    /// 清空当前事件列表
    /// </summary>
    public void Clear()
    {
        if(linkedList != null)
        {
            this.linkedList.Clear();
        }
    }

    /// <summary>
    /// 根据点击回调获取整个事件列表
    /// </summary>
    /// <param name="call"></param>
    /// <returns></returns>
    public SignalHandle GetHandle(SignalDelegate call)
    {
        if (linkedList == null)
        {
            linkedList = new LinkedList<SignalDelegate>();
        }
        LinkedListNode<SignalDelegate> linkedListNode = linkedList.AddLast(call);
        return new SignalHandle(this.linkedList, linkedListNode);
    }

    /// <summary>
    /// 执行当前eventName绑定的所有事件
    /// </summary>
    /// <param name="args"></param>
    public void Execute(params object[] args)
    {
        if (linkedList != null)
        {
            LinkedListNode<SignalDelegate> next;
            for (LinkedListNode<SignalDelegate> linkedListNode = linkedList.First; linkedListNode != null; linkedListNode = next)
            {
                next = linkedListNode.Next;
                SignalDelegate value = linkedListNode.Value;
                value(args);
            }
        }
    }
}

public delegate void SignalDelegate(params object[] arags);

/// <summary>
/// 单个事件列表释放执行器
/// </summary>
public class SignalHandle
{
    private LinkedList<SignalDelegate> linkedList;

    private LinkedListNode<SignalDelegate> linkedListNode;

    public SignalHandle(LinkedList<SignalDelegate> linkedList, LinkedListNode<SignalDelegate> linkedListNode)
    {
        this.linkedList = linkedList;
        this.linkedListNode = linkedListNode;
    }

    public void Dispose()
    {
        if (this.linkedList != null && this.linkedListNode != null)
        {
            this.linkedList.Remove(this.linkedListNode);
            this.linkedListNode = null;
            this.linkedList = null;
        }
    }
}
