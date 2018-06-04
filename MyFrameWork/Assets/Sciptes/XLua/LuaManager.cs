using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class LuaManager{

    LuaEnv luaenv;

    private LuaTable scriptEnv;

    private Action luaUpdate;
    private Action luaStop;
    private Action luaFocus;
    private Action luaPause;
    private Action<string> executeGm;
    private Action<string> collectgarbage;

    public LuaManager()
    {
        luaenv = new LuaEnv();
        luaenv.AddLoader(LuaLoader);
    }

    byte[] LuaLoader(ref string filepath)
    {
        string absPath = Application.dataPath + @"\Lua\" + filepath + ".lua";
        IPathTool.FixedPath(ref absPath);
        return File.ReadAllBytes(absPath);
    }

    public void InitStart(string enterPath)
    {
        scriptEnv = luaenv.NewTable();

        LuaTable meta = luaenv.NewTable();
        meta.Set("__index", luaenv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        scriptEnv.Set("self", this);
        try
        {
            luaenv.DoString("require '" + enterPath + "'", "LuaBehaviour", scriptEnv);
        }
        catch (LuaException exp)
        {
            Debug.LogError(exp.Message);
        }
        //table.Get 获取lua中的delegate以供调用
        scriptEnv.Get("Update", out luaUpdate);
        scriptEnv.Get("Stop", out luaStop);
        scriptEnv.Get("Focus", out luaFocus);
        scriptEnv.Get("Pause", out luaPause);
        scriptEnv.Get("ExecuteGm", out executeGm);
        scriptEnv.Get("Collectgarbage", out collectgarbage);
}

    public void Update()
    {
        if (luaUpdate != null)
        {
            luaUpdate();
        }
    }

    public void LuaStop()
    {
        if (luaStop != null)
        {
            luaStop();
        }
    }

    public void LuaFocus()
    {
        if (luaFocus != null)
        {
            luaFocus();
        }
    }

    public void LuaPause()
    {
        if (luaPause != null)
        {
            luaPause();
        }
    }

    public void ExecuteGm(string gm)
    {
        if (executeGm != null)
        {
            executeGm(gm);
        }
    }

    public void Collectgarbage(string param)
    {
        if (collectgarbage != null)
        {
            collectgarbage(param);
        }
    }

    public bool IsLuaEnv()
    {
        return luaenv != null;
    }

    /// <summary>
    /// 清除Lua的未手动释放的LuaBase对象
    /// </summary>
    public void Tick()
    {
        luaenv.Tick();
    }

    public void GC()
    {
        luaenv.GC();
    }

    /// <summary>
    /// 释放所有资源
    /// </summary>
    public void Close()
    {
        luaUpdate = null;
        luaStop = null;
        luaFocus = null;
        luaPause = null;
        executeGm = null;
        collectgarbage = null;
        scriptEnv.Dispose();
        //luaenv.Dispose();
    } 
}
