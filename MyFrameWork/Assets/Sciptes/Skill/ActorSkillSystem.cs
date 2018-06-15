using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameWork.Skill
{
    /// <summary>
    /// 技能外观类 供其他模块直接调用
    /// </summary>
    [RequireComponent(typeof(ActorSkillManager))]
    public class ActorSkillSystem : BaseBehaviour
    {
        /// <summary> 角色技能管理器 </summary>
        private ActorSkillManager skillManager;

        /// <summary> 角色动画控制器 </summary>
        private Animator anim;

        private SkillData currentSkill;

        protected override void Start()
        {
            skillManager = GetComponent<ActorSkillManager>();
            anim = GetComponentInChildren<Animator>();
            //注册动画事件
            GetComponentInChildren<AnimationEventBehaviour>().AttackHandler += DeploySkill;
        }

        /// <summary>
        /// 释放技能
        /// </summary>
        private void DeploySkill()
        {
            skillManager.GenerateSkill(currentSkill);
        }

        /// <summary>上一个攻击目标/// </summary>
        Transform lastAttackTarget;

        /// <summary>
        /// 攻击
        /// </summary>
        /// <param name="skillId">技能id</param>
        /// <param name="isBatter">是否连击</param>
        public void AttackUseSkill(int skillId, bool isBatter)
        {
            if (currentSkill != null && isBatter)
                skillId = currentSkill.nextBatterId;//当前currentSkill 是上一次释放的技能
            //准备当前的技能
            currentSkill = skillManager.GetPrepareSkill(skillId);
            //如果准备失败，退出
            if (currentSkill == null) return;
            //播放当前技能动画
            anim.SetBool(currentSkill.animationName, true);
            if (currentSkill.attackType != SkillAttackType.Single) return;

            //如果是单体攻击
            var currentTarget = SelectTarget(currentSkill);
            if (currentTarget == null) return;

            //转向敌人
            transform.LookPosition(currentTarget.position);

            ShowSelectedTarget(false);
            //替换上一个敌人引用
            lastAttackTarget = currentTarget;
            //选中当前敌人
            ShowSelectedTarget(true);
        }

        /// <summary>
        /// 显示目标选中特效
        /// </summary>
        /// <param name="state"></param>
        private void ShowSelectedTarget(bool state)
        {
            if (lastAttackTarget == null) return;
            var selected = lastAttackTarget.GetComponentInChildren<ActorSelected>();
            if (selected != null) selected.SetSelectedActive(state);
        }

        /// <summary>
        /// 选择目标
        /// </summary>
        /// <returns></returns>
        private Transform SelectTarget(SkillData currentSkill)
        {
            //通过当前技能的预制体 拿到 释放器
            var targets = DeployerConfigFactory.CreateAttackSelector(currentSkill).DoSelect(currentSkill, transform);
            return targets.Length != 0 ? targets[0] : null;
        }

        /// <summary>
        /// 随机使用技能（怪物或者角色挂机使用）
        /// </summary>
        public void UseRandomSkill()
        {
            var useableSkill = skillManager.skills.FindAll(s =>
                s.coolRemain <= 0 &&
                s.costSP <= GetComponent<ActorStatus>().SP
            );

            if(useableSkill != null && useableSkill.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, useableSkill.Count);
                int id = useableSkill[index].skillId;
                AttackUseSkill(id, false);
            }
        }
    }
}