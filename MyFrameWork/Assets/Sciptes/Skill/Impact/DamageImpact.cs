using System.Collections;
using System.Collections.Generic;
using MyFrameWork.Skill;
using UnityEngine;

namespace MyFrameWork.Skill
{
    public class DamageImpact : IImpact
    {
        public void DoImpact(SkillDeployer deployer)
        {
            deployer.StartCoroutine(RepeatAttack(deployer));
        }

        /// <summary>
        /// 持续伤害的技能（如果持续时间=攻击间隔） 则调用一次
        /// </summary>
        /// <param name="deployer"></param>
        /// <returns></returns>
        private IEnumerator RepeatAttack(SkillDeployer deployer)
        {
            float atkTime = 0;
            do
            {
                //单次攻击
                OnceAttack(deployer.CurrentSkillData);
                //等待攻击间隔
                yield return new WaitForSeconds(deployer.CurrentSkillData.atkInterval);
                //计时
                atkTime += deployer.CurrentSkillData.atkInterval;
                //每次攻击完重新计算目标
                deployer.CalculateTargets();
            } while (atkTime < deployer.CurrentSkillData.durationTime);
        }

        private void OnceAttack(SkillData skill)
        {
            //角色攻击力
            float atk = skill.atkRatio * skill.onwer.GetComponent<ActorStatus>().baseATK;

            for (int i = 0; i < skill.attackTargets.Length; i++)
            {
                skill.attackTargets[i].GetComponent<ActorStatus>().Damage(atk);
            }
        }
    }
}
