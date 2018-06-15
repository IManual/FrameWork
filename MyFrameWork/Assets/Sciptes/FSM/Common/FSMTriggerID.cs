using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FSMTriggerID
{
    //生命为0	
    NoHealth,
    //生命低于30%
    LackHealth,
    //到达血池
    Arrive,
    //发现目标	
    SawTarget,
    //目标进入攻击范围	
    ReachTarget,
    //丢失玩家	
    LoseTarget,
    //完成巡逻	
    CompletePatrol,
    //打死目标	
    KilledTarget,
    //目标不在攻击范围  玩家离开攻击范围	
    WithoutAttackRange
}
