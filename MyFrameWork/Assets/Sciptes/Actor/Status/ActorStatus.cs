using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ActorStatus : MonoBehaviour
{
    public ActorAnimationParameter animParams;

    public float attackDistance = 2;

    public float attackInterval = 2;

    public float baseATK = 20;

    public float defence = 5;

    public float HP = 100;

    public float maxHP = 100;

    public float maxSP = 1000;

    public float SP = 1000;

    public float runSpeed = 2;

    public float walkSpeed = 1;

    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// 是否在攻击
    /// </summary>
    /// <returns></returns>
    public bool IsAttacking()
    {
        return anim.GetBool(animParams.Attack01) || anim.GetBool(animParams.Attack02) || anim.GetBool(animParams.Attack03);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="val"></param>
    public void Damage(float val)
    {
        //扣除防御力
        val -= defence;
        if (val >= 0)
        {
            HP -= val;
            if (HP <= 0) Death();
        }
    }

    public virtual void Death()
    {
        GetComponentInChildren<Animator>().SetBool(animParams.DeathName, true);
    }

}
