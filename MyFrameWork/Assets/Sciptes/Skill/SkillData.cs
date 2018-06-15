using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameWork.Skill
{
    /// <summary>
    /// 技能数据类
    /// </summary>
    [Serializable]
    public class SkillData
    {
        /// <summary>技能ID</summary>
        public int skillId;

        /// <summary>技能名称</summary>
        public string skillName;

        /// <summary>技能描述</summary>
        public string description;

        /// <summary>冷却时间</summary>
        public float coolTime;

        /// <summary>冷却剩余时间</summary>
        public float coolRemain;

        /// <summary>魔法消耗</summary>
        public float costSP;

        /// <summary>攻击距离</summary>
        public float attackDistance;

        /// <summary>攻击角度</summary>
        public float attackAngle;

        /// <summary>开启等级</summary>
        public int unlockLevel;

        /// <summary>攻击目标标签</summary>
        public string[] attackTargetTags;

        /// <summary>攻击目标的变换组件</summary>
        [HideInInspector]
        public Transform[] attackTargets;

        /// <summary>技能影响类型(可多种)</summary>
        public string[] impactType ;

        /// <summary>连击的下一个技能编号</summary>
        public int nextBatterId;

        /// <summary>伤害比率</summary>
        public float atkRatio;

        /// <summary>持续时间</summary>
        public float durationTime;

        /// <summary>伤害间隔</summary>
        public float atkInterval;

        /// <summary>技能所属</summary>
        [HideInInspector]
        public GameObject onwer;

        /// <summary>技能预制体名称</summary>
        public string prefabName;

        /// <summary>预制体对象</summary>
        [HideInInspector]
        public GameObject skillPrefab;

        /// <summary>对应动画名称</summary>
        public string animationName;

        /// <summary>受击特效名称</summary>
        public string hitFxName;

        /// <summary>受击特效预制体</summary>
        [HideInInspector]
        public GameObject hitFxPrefab;

        /// <summary>技能等级</summary>
        public int skillLevle;

        /// <summary>技能类型</summary>
        public SkillAttackType attackType;

        /// <summary>技能伤害范围模式</summary>
        public SelectorType selectorType;
    }
}