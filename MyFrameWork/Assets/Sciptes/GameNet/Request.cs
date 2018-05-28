using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using XLua;

[LuaCallCSharp]
public abstract class Request
{
    public byte opCode;

    public abstract void OnOperationResponse(OperationResponse operationResponse); //响应回调

    public abstract void DefaltRequest(); // 请求

    private Request request;

    public Request(Request request, byte opCode)
    {
        this.request = request;
        this.opCode = opCode;
        GameNet.Instance.RegisterRequest(request);
    }
}
