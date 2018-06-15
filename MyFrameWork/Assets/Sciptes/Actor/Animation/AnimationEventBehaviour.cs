using System;
using UnityEngine;

public class AnimationEventBehaviour : BaseBehaviour
{
    public event Action AttackHandler;

    private Animator anim;

    protected override void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// 挂载到动画片段
    /// </summary>
    public void OnCancelAnim(string animParam)
    {
        anim.SetBool(animParam, false);
    }

    /// <summary>
    /// 挂载到动画片段
    /// </summary>
    public void OnAttack()
    {
        if (AttackHandler != null)
        {
            AttackHandler();
        }
    }
}
