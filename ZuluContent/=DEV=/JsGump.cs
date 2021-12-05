using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Server.Network;

namespace Server.Gumps;

public record JsGumpData<T>(string Type, T Data);

public class JsGump<TParams, TResponse> : Gump, INotifyCompletion where TParams : class where TResponse : class
{
    private readonly Mobile m_Player;

    public JsGump(string type, Mobile player, TParams data) : base(default, default)
    {
        m_Player = player;
        Add(new GumpJsonEntry(player, new JsGumpData<TParams>(type, data)));
    }
    
    public override void OnResponse(NetState sender, RelayInfo info)
    {
        if (typeof(TResponse) == typeof(string))
        {
            ResponseData = info.TextEntries.FirstOrDefault()?.Text as TResponse;
        }
        
        IsCompleted = true;
        Continuation?.Invoke();
    }
        
    #region INotifyCompletion
        
    protected Action Continuation;
    public bool IsCompleted { get; private set; }
    public virtual JsGump<TParams, TResponse> GetAwaiter() => this;
    public TResponse GetResult() => ResponseData;
    public TResponse ResponseData { get; private set; }

    public void OnCompleted(Action continuation)
    {
        if (!IsCompleted && ResponseData == null)
        {
            Continuation = continuation;
            m_Player?.SendGump(this);
        }
        else
        {
            continuation();
        }
    }

    #endregion
}