using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public delegate void ProtocolHandler(Dictionary<byte, object> msg);

public abstract class ProtocolBase
{
    public byte opCode;

    public ProtocolHandler handler = null;

    /// <summary>
    /// 响应回调
    /// </summary>
    /// <param name="operationResponse"></param>
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        if (handler != null)
        {
            handler(operationResponse.Parameters);
        }
    }

    public void OnEvent(EventData eventData)
    {
        if (handler != null)
        {
            handler(eventData.Parameters);
        }
    }

    public abstract void DefaltRequest(); // 请求

    private ProtocolBase protocol;

    public ProtocolBase(ProtocolBase protocol, byte opCode)
    {
        this.protocol = protocol;
        this.opCode = opCode;
        GameNet.Instance.RegisterProtocol(protocol);
    }
}
