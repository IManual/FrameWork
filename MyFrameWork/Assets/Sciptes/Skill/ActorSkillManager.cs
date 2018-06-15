using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameWork.Skill
{
    /// <summary>
    /// 技能管理器(负责管理单个角色所有技能)
    /// </summary>
    public class ActorSkillManager : BaseBehaviour
    {
        /// <summary> 管理的所有技能 </summary>
        public List<SkillData> skills;

        protected override void Start()
        {
            //此处可通过配置表对所有技能数据进行初始化
            InitSkills();
        }

        #region 内部方法

        /// <summary>
        /// 预先加载角色要用的所有技能预制体 以及技能数据
        /// </summary>
        private void InitSkills()
        {
            //初始化数据
            InitSkillData();
            //加载预制体
            foreach (var item in skills)
            {
                item.skillPrefab = LoadPrefab(item.prefabName);
                item.onwer = gameObject;
            }
        }

        /// <summary>
        /// 通过配置表配置技能信息
        /// </summary>
        private void InitSkillData()
        {
            
        }

        /// <summary>
        /// 加载技能预制体
        /// </summary>
        /// <param name="prefabName"></param>
        /// <returns></returns>
        private GameObject LoadPrefab(string prefabName)
        {
            //加载到预制体
            var prefab = AssetLoaderManager.LoadAsset<GameObject>("uis/effect",prefabName);
            //通过对象池进行创建
            var skillGO = GameObjectPool.Instance.CreateObject(prefabName, prefab, Vector3.zero, Quaternion.identity);

            //回收物体
            GameObjectPool.Instance.CollectObject(skillGO);
            return prefab;
        }

        /// <summary>
        /// 生成技能（调用释放器  执行技能算法 技能开始冷却）
        /// </summary>
        /// <param name="skill"></param>
        public void GenerateSkill(SkillData skill)
        {
            var skillGO = GameObjectPool.Instance.CreateObject(skill.prefabName, skill.skillPrefab, transform.position, transform.rotation);

            //拿到当前技能挂载的释放器
            var deployer = skillGO.GetComponent<SkillDeployer>();
            //对释放器技能对象赋值（触发实际创建算法对象)
            deployer.CurrentSkillData = skill;
            //释放技能 执行伤害算法
            deployer.DeployerSkill();
            StartCoroutine(CoolTimeDown(skill));
        }

        /// <summary>
        /// 冷却时间倒计时
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        private IEnumerator CoolTimeDown(SkillData skill)
        {
            skill.coolRemain = skill.coolTime;
            while (skill.coolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                skill.coolRemain -= 1;
            }
        }

        #endregion

        #region 提供外部功能

        /// <summary>
        /// 获取技能剩余时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public float GetSkillCoolRemain(int id)
        {
            return skills.Find(s => s.skillId == id).coolRemain;
        }

        /// <summary>
        /// 获取到准备好的技能（未达到释放条件返回null）
        /// </summary>
        /// <param name="id">技能ID</param>
        /// <returns></returns>
        public SkillData GetPrepareSkill(int id)
        {
            //根据技能ID找到技能
            SkillData data = skills.Find(s => s.skillId == id);

            return data != null && data.coolRemain <= 0 && GetComponent<ActorStatus>().SP >= data.costSP ? data : null;
        }

        #endregion
    }
}
