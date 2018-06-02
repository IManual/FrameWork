using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class GameRoot2 : MonoSingleton<GameRoot2>
{

    LuaManager luaMnager;

    protected override void Awake()
    {
        luaMnager = new LuaManager();
        luaMnager.InitStart("utils/main");
        //初始化ctrl
        //MVCEntry.__init();

        // 监听低内存更新
        Application.lowMemory -= this.OnLowMemory;
        Application.lowMemory += this.OnLowMemory;
        // 检查并设置分辨率.
        this.LimitScreenResolution(1080);
    }

    protected override void Start()
    {
        //ViewManager.Instance.Open(ViewName.MainView);
    }

    protected override void Update()
    {
        luaMnager.Update();
        if (luaMnager.IsLuaEnv())
        {
            luaMnager.GC();
            luaMnager.Tick();
        }
    }

    private void OnLowMemory()
    {
        //清理所有未使用的资源
        Collectgarbage("collect");

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
        if (Screen.width > Screen.height)
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
        //ViewManager.Instance.InitViewObject();
    }


    protected override void OnDestroy()
    {
        
    }

    protected override void OnApplicationQuit()
    {
        if (luaMnager.IsLuaEnv())
        {
            luaMnager.LuaStop();
        }
        luaMnager.Close();
    }

    [CSharpCallLua]
    public void ExecuteGm(string gm)
    {
        luaMnager.ExecuteGm(gm);
    }

    [CSharpCallLua]
    public void Collectgarbage(string param)
    {
        luaMnager.Collectgarbage(param);
    }
}

