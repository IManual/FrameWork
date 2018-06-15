using System;

/// <summary>
/// 角色动画参数
/// </summary>
[Serializable]
public class ActorAnimationParameter
{
    /// <summary>
    /// 待机
    /// </summary>
    public string IdleName = "idle";
    /// <summary>
    /// 跑步
    /// </summary>
    public string RunName = "run";
    /// <summary>
    /// 死亡
    /// </summary>
    public string DeathName = "death";
    /// <summary>
    /// 走路
    /// </summary>
    public string WalkName = "walk";
    /// <summary>
    /// 攻击1
    /// </summary>
    public string Attack01 = "attack1";
    /// <summary>
    /// 攻击2
    /// </summary>
    public string Attack02 = "attack2";
    /// <summary>
    /// 攻击3
    /// </summary>
    public string Attack03 = "attack3";
}