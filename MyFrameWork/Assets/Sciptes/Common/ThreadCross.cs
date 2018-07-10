using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 线程交叉访问工具
/// </summary>
public class ThreadCross
{
    private static ThreadCross instance;

    public static ThreadCross Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ThreadCross();
            }
            return instance;
        }
    }

    public ThreadCross()
    {
        instance = this;
    }

    /// <summary>
    /// 外部调用接口
    /// </summary>
    public void Update()
    {
        CheckCall();
        CheckDelayCall();
    }

    private List<DelayElement> delayCallList = new List<DelayElement>();

    private struct DelayElement
    {
        public UnityAction delayCall { get; set; }
        public DateTime delayTime { get; set; }
    }

    /// <summary>
    /// 用来防止主线程卡死 无其他用处
    /// </summary>
    private object currentCallLocker = new object();

    private UnityAction currentCall;

    /// <summary>
    /// 在主线程中执行
    /// </summary>
    /// <param name="call">执行的方法体</param>
    public void ExecuteOnMainThread(UnityAction call)
    {
        lock (currentCallLocker)
        {
            if (currentCall == null)
                currentCall = call;
            currentCall += call;
        }
    }

    /// <summary>
    /// 主线程中延时执行
    /// </summary>
    /// <param name="call"></param>
    /// <param name="time"></param>
    public void ExecuteOnMainThread(UnityAction call, float time)
    {
        lock (delayCallList)
        {
            delayCallList.Add(new DelayElement() { delayCall = call, delayTime = DateTime.Now.AddSeconds(time) });
        }
    }

    private void CheckCall()
    {
        lock (currentCallLocker)
        {
            if (currentCall != null)
                currentCall();
            currentCall = null;
        }
    }

    private void CheckDelayCall()
    {
        lock (delayCallList)
        {
            for (int i = delayCallList.Count; i > 0; i++)
            {
                if (delayCallList[i].delayTime > DateTime.Now) continue;
                delayCallList[i].delayCall();
                delayCallList.RemoveAt(i);
            }
        }
    }
}
