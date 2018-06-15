using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameWork.Skill
{
    /// <summary>
    /// 扇形选区实现类
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        public Transform[] DoSelect(SkillData data, Transform skillTf)
        {
            //获取周围攻击目标 通过标签 角度
            var attackTargets = skillTf.CalculateAroundObjects(data.attackTargetTags, data.attackDistance, data.attackAngle);
            //需满足血量大于0
            attackTargets = ArrayHelper.FindAll(attackTargets, t => t.GetComponent<ActorStatus>().HP > 0);
            //根据攻击类型选择目标
            if (data.attackType == SkillAttackType.Single)
            {
                if (attackTargets.Length == 0) return new Transform[0];
                return new Transform[1] {
                    ArrayHelper.GetMin(attackTargets, t => Vector3.Distance(t.position, skillTf.position))
                };
            }
            else { return attackTargets; }
        }
    }
}
