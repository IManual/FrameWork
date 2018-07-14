using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBehaviour : MonoBehaviour
{
    /// <summary>
    /// 当该脚本被加载活检视面板的值被修改时调用此函数(仅在编辑器中调用)
    /// </summary>
    protected virtual void OnValidate()
    {
        
    }

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void OnDestroy()
    {

    }

    protected virtual void OnApplicationQuit()
    {

    }
}
