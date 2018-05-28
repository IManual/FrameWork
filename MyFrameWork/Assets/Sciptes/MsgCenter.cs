using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消息分发中心
/// </summary>
public class MsgCenter : BaseManager<MsgCenter> {

    /// <summary>
    /// 给对应模块发送消息
    /// </summary>
	public void SendToMsg(MsgBase tmpMsg)
    {
        AnasysisMsg(tmpMsg);
    }

    /// <summary>
    /// 消息分发
    /// </summary>
    private void AnasysisMsg(MsgBase tmpMsg)
    {
        ManagerID tmpId = tmpMsg.GetManager();
        switch (tmpId)
        {
            case ManagerID.GameManager:
                GameManager.Instance.ProcessEvent(tmpMsg);
                break;
            case ManagerID.ViewManager:
                ViewManager.Instance.ProcessEvent(tmpMsg);
                break;
            case ManagerID.AudioManager:
                AudioManager.Instance.ProcessEvent(tmpMsg);
                break;
            case ManagerID.NPCManager:
                NPCManager.Instance.ProcessEvent(tmpMsg);
                break;
            case ManagerID.CharacterManager:
                CharacterManager.Instance.ProcessEvent(tmpMsg);
                break;
            case ManagerID.AssetManager:
                AssetManager.Instance.ProcessEvent(tmpMsg);
                break;
            case ManagerID.NetManager:
                NetManager.Instance.ProcessEvent(tmpMsg);
                break;
            default:Debug.LogError("Wrong Msg");
                break;
        }

    }
}
