using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Scripts.Zulu.Packets;
using Server.Network;

namespace Server.Gumps;

public class WebGumpRequest<TRequest, TResponse> : INotifyCompletion where TRequest : class where TResponse : class
{
    private readonly WebGump m_Gump;
    public TRequest RequestData { get; }
    private readonly NetState m_Owner;

    public WebGumpRequest(WebGump gump, TRequest requestData, NetState owner)
    {
        m_Gump = gump;
        RequestData = requestData;
        m_Owner = owner;
    }

    public void Send()
    {
        m_Gump?.SendTo(m_Owner);
        IsCompleted = true;
    }

    public void SetResponseBody(string body)
    {
        if (IsCompleted)
            return;

        if (!string.IsNullOrEmpty(body))
        {
            try
            {
                ResponseData = JsonSerializer.Deserialize<TResponse>(body, OutgoingZuluPackets.SerializerOptions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        else
        {
            ResponseData = null;
        }


        IsCompleted = true;
        Continuation?.Invoke();
    }
    
    #region INotifyCompletion

    protected Action Continuation;
    public bool IsCompleted { get; private set; }
    public virtual WebGumpRequest<TRequest, TResponse> GetAwaiter() => this;
    public TResponse GetResult() => ResponseData;
    public TResponse ResponseData { get; private set; }

    public void OnCompleted(Action continuation)
    {
        if (!IsCompleted && ResponseData == null)
        {
            Continuation = continuation;
            m_Gump?.SendTo(m_Owner); 
        }
        else
        {
            continuation?.Invoke();
        }
    }

    #endregion
}