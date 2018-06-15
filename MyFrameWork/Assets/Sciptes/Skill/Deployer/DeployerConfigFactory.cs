using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameWork.Skill
{
    /// <summary>
    /// 技能伤害 影响算法工厂（创建实际对象）
    /// </summary>
    public static class DeployerConfigFactory
    {
        /// <summary>算法对象缓存</summary>
        private static Dictionary<string, object> cache;

        static DeployerConfigFactory()
        {
            cache = new Dictionary<string, object>();
        }

        /// <summary>
        /// 创建技能选区对象
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public static IAttackSelector CreateAttackSelector(SkillData skill)
        {
            return CreatObject<IAttackSelector>("MyFrameWork.Skill." + skill.selectorType + "AttackSelector");
        }

        /// <summary>
        /// 创建技能影响对象列表
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public static List<IImpact> CreateImpact(SkillData skill)
        {
            List<IImpact> impactList = new List<IImpact>(skill.impactType.Length);
            for (int i = 0; i < skill.impactType.Length; i++)
            {
                impactList.Add(CreatObject<IImpact>("MyFrameWork.Skill." + skill.impactType[i] + "Impact"));
            }
            return impactList;
        }

        /// <summary>
        /// 通过字符串创建类对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classNameAll"></param>
        /// <returns></returns>
        private static T CreatObject<T>(string className)where T : class
        {
            //如果池中不存在对象
            if (!cache.ContainsKey(className))
            {
                //反射创建对象
                Type type = Type.GetType(className);
                cache.Add(className, Activator.CreateInstance(type) as T);
            }
            //从缓存中返回对象引用
            return cache[className] as T;
        }
    }
}