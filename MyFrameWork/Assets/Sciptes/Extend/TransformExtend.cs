using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public static class TransformExtend
{
    /// <summary>
    /// 设置物体缩放
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void SetLocalScale(this Transform self, float x, float y, float z)
    {
        self.localScale = new Vector3(x, y, z);
    }

    /// <summary>
    /// 查找子物体
    /// </summary>
    /// <param name="self"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Transform FindHard(this Transform self, string path)
    {
        Transform tf = self.Find(path);
        if (tf == null)
        {
            for (int i = 0; i < self.childCount; i++)
            {
                tf = self.GetChild(i).FindHard(path);
                if (tf != null) return tf;
            } 
        }
        else
        {
            return tf;
        }
        return null;
    }

    /// <summary>
    /// 注释目标点旋转(lerp)
    /// </summary>
    /// <param name="self"></param>
    /// <param name="targetPos"></param>
    /// <param name="rotateSpeed"></param>
    public static void LookPosition(this Transform self,Vector3 targetPos,float rotateSpeed)
    {
        //获取两点之间方向
        Vector3 dir = targetPos - self.position;
        //转向获取到的方向
        self.LookDirection(dir, rotateSpeed);
    }

    /// <summary>
    /// 旋转至目标点
    /// </summary>
    /// <param name="self"></param>
    /// <param name="targetPos"></param>
    public static void LookPosition(this Transform self, Vector3 targetPos)
    {
        //获取两点之间方向
        Vector3 dir = targetPos - self.position;
        //转向获取到的方向
        self.LookDirection(dir);
    }

    /// <summary>
    /// 注释目标方向逐渐旋转(lerp效果)
    /// </summary>
    /// <param name="self"></param>
    /// <param name="dir"></param>
    /// <param name="rotateSpeed"></param>
    public static void LookDirection(this Transform self, Vector3 targetDir, float rotateSpeed)
    {
        Quaternion dir = Quaternion.LookRotation(targetDir);
        self.rotation = Quaternion.Lerp(self.rotation, dir, Time.deltaTime * rotateSpeed);
    }

    /// <summary>
    /// 旋转至目标方向
    /// </summary>
    /// <param name="self"></param>
    /// <param name="targetDir"></param>
    public static void LookDirection(this Transform self, Vector3 targetDir)
    {
        Quaternion dir = Quaternion.LookRotation(targetDir);
        self.rotation = Quaternion.Lerp(self.rotation, dir, 1f);
    }

    /// <summary>
    /// 查找当前物体周边物体（后期修改：添加场景管理类，由场景管理类获取）
    /// </summary>
    /// <param name="self"></param>
    /// <param name="tags"></param>
    /// <param name="distance"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static Transform[] CalculateAroundObjects(this Transform self, string[] tags, float distance, float angle)
    {
        List<Transform> result = new List<Transform>();
        for (int i = 0; i < tags.Length; i++)
        {
            GameObject[] allGO = GameObject.FindGameObjectsWithTag(tags[i]);

            if (allGO == null) continue;
            Transform[] allTF = ArrayHelper.Select(allGO, o => o.transform);
            result.AddRange(allTF);
        }
        result = result.FindAll(tf => Vector3.Distance(tf.position, self.position) < distance &&
        Vector3.Angle(self.forward, tf.position - self.position) <= angle / 2);
        return result.ToArray();
    } 
}
