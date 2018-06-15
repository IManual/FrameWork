using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameWork.Skill
{
    /// <summary>
    /// 抽象技能释放器（挂载到技能身上）
    /// </summary>
    public abstract class SkillDeployer : BaseBehaviour
    {
        private SkillData currentSkillData;

        /// <summary>
        /// 当前技能数据
        /// </summary>
        public SkillData CurrentSkillData
        {
            get { return currentSkillData; }

            set
            {
                currentSkillData = value;
                //给技能释放器 技能对象 赋值时  创建相应的伤害算法、选区算法
                ConfigDeployer();
            }
        }

        /// <summary>选区算法对象/// </summary>
        protected IAttackSelector selector;

        /// <summary>伤害影响效果列表</summary> 
        protected List<IImpact> impactList;

        /// <summary>
        /// 选区算法对象 伤害影响效果对象创建
        /// </summary>
        private void ConfigDeployer()
        {
            selector = DeployerConfigFactory.CreateAttackSelector(currentSkillData);
            impactList = DeployerConfigFactory.CreateImpact(currentSkillData);
        }

        #region 子类重写

        /// <summary>
        /// 释放技能
        /// </summary>
        public abstract void DeployerSkill();

        #endregion

        #region 提供的功能

        /// <summary>
        /// 查找被选中目标
        /// </summary>
        public void CalculateTargets()
        {
            currentSkillData.attackTargets = selector.DoSelect(currentSkillData, transform);
        }

        /// <summary>
        /// 回收技能
        /// </summary>
        protected void CollectSkill()
        {
            GameObjectPool.Instance.CollectObject(gameObject, currentSkillData.durationTime);
        }

        #endregion
    }
}
