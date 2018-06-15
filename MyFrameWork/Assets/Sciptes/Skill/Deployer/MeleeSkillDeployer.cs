using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameWork.Skill
{
    /// <summary>
    /// 近身技能释放器
    /// </summary>
    public class MeleeSkillDeployer : SkillDeployer
    {
        public override void DeployerSkill()
        {
            //回收当前技能
            base.CollectSkill();
            //查找目标
            base.CalculateTargets();

            impactList.ForEach(i => i.DoImpact(this));
        }
    }
}