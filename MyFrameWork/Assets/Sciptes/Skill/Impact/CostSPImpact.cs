using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameWork.Skill
{
    /// <summary>
    /// 消耗自身SP
    /// </summary>
    public class CostSPImpact : IImpact
    {
        public void DoImpact(SkillDeployer deployer)
        {
            deployer.CurrentSkillData.onwer.GetComponent<PlayerStatus>().SP -= deployer.CurrentSkillData.costSP;
        }
    }
}