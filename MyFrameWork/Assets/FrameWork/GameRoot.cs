using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : BaseManager<GameRoot>
{

    protected override void Awake()
    {
        //初始化ctrl
        MVCEntry.__init();
        gameObject.AddComponent<GlobalTimeRequest>();

        // 监听低内存更新
        Application.lowMemory -= this.OnLowMemory;
        Application.lowMemory += this.OnLowMemory;
        // 检查并设置分辨率.
        this.LimitScreenResolution(1080);
    }

    private void OnLowMemory()
    {
        //清理所有未使用的资源

        // Clear the unity memory.
        Resources.UnloadUnusedAssets();
#if !UNITY_EDITOR
        GC.Collect();
#endif
    }

    /// <summary>
    /// 限制屏幕分辨率
    /// </summary>
    /// <param name="limit"></param>
    private void LimitScreenResolution(int limit)
    {
        if(Screen.width > Screen.height)
        {
            if (Screen.height > limit)
            {
                var radio = (float)Screen.width / Screen.height;
                Screen.SetResolution((int)(limit * radio), limit, true);
            }
        }
        else
        {
            if (Screen.width > limit)
            {
                var radio = (float)Screen.width / Screen.height;
                Screen.SetResolution(limit, (int)(limit * radio), true);
            }
        }
    }

    protected override void OnEnable()
    {
        //初始化预制件
        ViewManager.Instance.InitViewObject();
    }

    protected override void Start()
    {       
        ViewManager.Instance.Open(ViewName.MainView);
    }

    protected override void OnApplicationQuit()
    {
        MVCEntry.__delete();
    }

    protected override void OnDestroy()
    {

    }
}
