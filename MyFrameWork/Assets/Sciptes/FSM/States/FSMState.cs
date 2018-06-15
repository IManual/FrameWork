using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState : BaseBehaviour
{
    public FSMStateID stateID;

    private List<FSMTrigger> triggers = new List<FSMTrigger>();

    /// <summary>映射表/// </summary>
    private Dictionary<FSMTriggerID, FSMStateID> map = new Dictionary<FSMTriggerID, FSMStateID>();

    public FSMState(FSMStateID stateID)
    {
        this.stateID = stateID;
    }

    /// <summary>
    /// 添加当前状态的所对应的跳转条件的映射关系
    /// </summary>
    /// <param name="triggerID"></param>
    /// <param name="stateID"></param>
    public void AddMap(FSMTriggerID triggerID, FSMStateID stateID)
    {
        //将编号存入映射表
        map.Add(triggerID, stateID);
        //创建条件对象
        triggers.Add(CreatTriggerObject(triggerID));
    }

    /// <summary>
    /// 根据triggerId创建trigger对象
    /// </summary>
    /// <param name="triggerID"></param>
    /// <returns></returns>
    private FSMTrigger CreatTriggerObject(FSMTriggerID triggerID)
    {
        Type type = Type.GetType(triggerID + "Trigger");
        return Activator.CreateInstance(type) as FSMTrigger;
    }

    /// <summary>
    /// 移除映射对象
    /// </summary>
    /// <param name="triggerID"></param>
    public void RemoveMap(FSMTriggerID triggerID)
    {
        if (map.ContainsKey(triggerID))
        {
            map.Remove(triggerID);
        }
    }

    /// <summary>
    /// 状态切换的条件检测
    /// </summary>
    /// <param name="fsm"></param>
    public void Reason(BaseFsm fsm)
    {
        //遍历当前状态的所有可跳转条件
        foreach (var item in triggers)
        {
            //判断是满足
            if (item.HandleTrigger(fsm))
            {
                //如果满足 回调状态机状态切换方法
                FSMStateID stateID = map[item.triggerID];
                fsm.ChangeActiveStateCallBack(stateID);
                return;
            }
        }
    }

    #region 子类重写

    public abstract void EnterAction(BaseFsm baseFsm);

    public abstract void OnAction(BaseFsm baseFsm);

    public abstract void ExitAction(BaseFsm baseFsm);

    #endregion
}
