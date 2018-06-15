using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameWork.Skill
{
    /// <summary>
    /// 技能选区
    /// </summary>
    public interface IAttackSelector
    {
        Transform[] DoSelect(SkillData data, Transform skillTf);
    }
}