using MyFrameWork.Skill;
using UnityEngine;

public class ActorInputController : BaseBehaviour
{
    public float moveSpeed = 3f;

    private Animator anim;

    /// <summary>
    /// 初始化所有组件
    /// </summary>
    private void Init()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected override void Awake()
    {
        Init();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        MoveControlByTranslateGetAxis();

        if (Input.GetKeyDown(KeyCode.J))
        {
            OnSkillDown(1000);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {

        }
        if (Input.GetKeyDown(KeyCode.L))
        {

        }
    }

    /// <summary>
    /// 释放技能 非单体技能
    /// </summary>
    private void OnSkillDown(int skillId)
    {
        anim.Play("swordStrike1");
    }

    public float maxBatterTime = 3;
    public float minAttackTime = 1;
    float lastClickTime;

    private void MoveControlByTranslateGetAxis()
    {
        float horizontal = Input.GetAxis("Horizontal"); //A D 左右
        float vertical = Input.GetAxis("Vertical"); //W S 上 下

        transform.Translate(Vector3.forward * vertical * moveSpeed * Time.deltaTime);//W S 上 下
        transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);//A D 左右
    }
}