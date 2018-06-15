using MyFrameWork.Skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseFsm : BaseBehaviour {

    protected override void Awake()
    {
        ConfigFSM();
        InitComponent();
        InitDefaultState();
    }

    protected override void Update()
    {
        //寻找目标
        SearchTarget();
        //进行切换的判断
        currentState.Reason(this);

        currentState.OnAction(this);
    }

    /// <summary> 状态列表/// </summary>
    private List<FSMState> states;

    [Tooltip("默认状态ID")]
    public FSMStateID defaultStateID;
    private FSMState defaultState;

    /// <summary>当前你状态/// </summary>
    private FSMState currentState;

    /// <summary>
    /// 初始化默认状态
    /// </summary>
    private void InitDefaultState()
    {
        defaultState = states.Find(s => s.stateID == defaultStateID);

        currentState = defaultState;

        currentState.EnterAction(this);
    }

    [HideInInspector]
    public Animator anim;

    [HideInInspector]
    public ActorStatus actorStatus;

    public ActorSkillSystem skillSystem;

    /// <summary>
    /// 初始化组件
    /// </summary>
    private void InitComponent()
    {
        anim = GetComponentInChildren<Animator>();
        actorStatus = GetComponent<ActorStatus>();
        nav = GetComponent<NavMeshAgent>();
        skillSystem = GetComponent<ActorSkillSystem>();
    }

    /// <summary>
    /// 配置状态机信息(添加映射关系)
    /// </summary>
    private void ConfigFSM()
    {
        states = new List<FSMState>();
        var dic = AIResourceManager.BuildDic();
        foreach (var stateName in dic.Keys)
        {
            FSMState state = CreatStateObject(stateName);
            states.Add(state);
            foreach (var map in dic[stateName])
            {
                var triggerID = (FSMTriggerID)Enum.Parse(typeof(FSMTriggerID), map.Key);
                var stateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), map.Value);
                state.AddMap(triggerID, stateID);
            }
        }
    }

    /// <summary>
    /// 创建state对象
    /// </summary>
    /// <param name="stateName"></param>
    /// <returns></returns>
    private FSMState CreatStateObject(string stateName)
    {
        Type type = Type.GetType(stateName + "State");
        return Activator.CreateInstance(type) as FSMState;
    }

    /// <summary>
    /// 改变当前状态回调
    /// </summary>
    /// <param name="stateID"></param>
    public void ChangeActiveStateCallBack(FSMStateID stateID)
    {
        FSMState nextState = stateID == FSMStateID.Default ? defaultState : states.Find(s => s.stateID == stateID);
        //退出当前状态
        currentState.ExitAction(this);
        //切换状态
        currentState = nextState;
        //进入下一状态
        nextState.EnterAction(this);
    }

    [HideInInspector]
    public NavMeshAgent nav;

    #region 向外部提供的成员变量

    public string[] targetTags;

    public Transform currentTarget;

    public float viewDistance;

    public float viewAngle;

    #endregion

    #region 向外部提供的成员方法

    /// <summary>
    /// 网格寻路
    /// </summary>
    /// <param name="pos"></param>
    /// <param name=""></param>
    public void MoveToTarget(Vector3 pos, float moveSpeed, float stopDistance, float rotateSpeed)
    {
        nav.speed = moveSpeed;
        nav.stoppingDistance = stopDistance;
        nav.SetDestination(pos);
        transform.LookPosition(pos, rotateSpeed);
    }

    /// <summary>
    /// 停止寻路
    /// </summary>
    public void StopMove()
    {
        nav.enabled = false;
        nav.enabled = true;
    }

    /// <summary>
    /// 查找视野内的敌人
    /// </summary>
    public void SearchTarget()
    {
        var allTargets = transform.CalculateAroundObjects(targetTags, viewDistance, viewAngle);
        //选择附近所有
        allTargets = ArrayHelper.FindAll(allTargets, a => a.GetComponent<ActorStatus>().HP > 0);

        if (allTargets.Length > 0)
            currentTarget = ArrayHelper.GetMin(allTargets, a => Vector3.Distance(a.position, transform.position));
        else currentTarget = null;
    }

    #endregion
}
