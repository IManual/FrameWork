using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using XLua;
using System;
using UnityEngine.Events;

[LuaCallCSharp]
public class GameNet : IPhotonPeerListener
{
    public UnityAction OnDisconnect;

    public UnityAction OnSendError;

    public UnityAction OnReConnect;

    private static GameNet instance;

    public static GameNet Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameNet();
            }
            return instance;
        }
    }

    public GameNet()
    {
        InitPeer();
        if (instance == null) instance = this;
        else Debug.LogError("Try To Instance [GameNet] Twice!");
    }

    private static PhotonPeer peer;

    public static PhotonPeer Peer
    {
        get { return peer; }
    }

    private void InitPeer()
    {
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        peer.Connect("127.0.0.1:5055", "MyGame1");
    }

    //协议池
    private Dictionary<byte, Request> requestPool = new Dictionary<byte, Request>();

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    //服务器主动发送消息时回调
    public void OnEvent(EventData eventData)
    {
        
        //BaseEvent e = 
    }

    //服务端响应回调
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        Request request = null; //创建空的操作类型对象
        bool temp = requestPool.TryGetValue(operationResponse.OperationCode, out request);
        if (temp)
            request.OnOperationResponse(operationResponse);
        else
            Debug.LogError("Error: no operation instance");
    }

    /// <summary>
    /// 持续请求服务连接
    /// </summary>
    public void Update()
    {
        //连接服务器时要一直调用service方法
        if(peer!= null)
        {
            peer.Service();
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log("Current Net State--------------" + statusCode);
        if (statusCode == StatusCode.SendError) { if (OnSendError != null) OnSendError(); }
        else if (statusCode == StatusCode.Disconnect) { if (OnDisconnect != null) OnDisconnect(); }
    }

    public void SetOnSendErrorCallBack(UnityAction call)
    {
        if (OnSendError == null) OnSendError = call; else OnSendError += call;
    }

    public void SetOnDisconnectCallBack(UnityAction call)
    {
        if (OnDisconnect == null) OnDisconnect = call; else OnDisconnect += call;
    }

    public void SetOnReConnectCallBack(UnityAction call)
    {
        if (OnReConnect == null) OnReConnect = call; else OnReConnect += call;
    }

    /// <summary>
    /// 重新连接
    /// </summary>
    public void ReConnect()
    {
        if (OnReConnect != null)
            OnReConnect();
    }

    /// <summary>
    /// 注册协议
    /// </summary>
    /// <param name="request"></param>
    public void RegisterRequest(Request request)
    {
        if (requestPool.ContainsKey(request.opCode))
        {
            return;
        }
        requestPool.Add(request.opCode, request);
    }

    public void DisConnected()
    { //游戏关闭时断开连接
        if (peer != null && peer.PeerState == PeerStateValue.Connected)
        {
            peer.Disconnect();
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Release()
    {
        OnDisconnect = null;
        OnSendError = null;
        OnReConnect = null;

        RemoveAllRequest();
        RemoveAllEvent();
    }

    private void RemoveAllRequest()
    {

    }

    private void RemoveAllEvent()
    {

    }
}
