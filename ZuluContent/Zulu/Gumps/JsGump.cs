using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Scripts.Zulu.Packets;
using Server.Json;
using Server.Network;
using static Scripts.Zulu.Utilities.ZuluUtil;

namespace Server.Gumps;

public record JsGumpData<T>(string Type, T Data);

public class JsGump<TParams, TResponse> : Gump, INotifyCompletion, IJsonGump where TParams : class where TResponse : class
{ 
    private readonly string m_Type;
    private readonly Mobile m_Player;
    private readonly TParams m_Data;

    public JsGump(string type, Mobile player, TParams data) : base(default, default)
    {
        m_Type = type;
        m_Player = player;
        m_Data = data;
    }

    public override void OnResponse(NetState sender, RelayInfo info)
    {
        //noop
    }

    protected virtual void SendGump()
    {
        m_Player.NetState.AddGump(this);
        OutgoingZuluPackets.SendJsGumpOpen(m_Player.NetState, this, new JsGumpData<TParams>(m_Type, m_Data));
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
            SendGump();
        }
        else
        {
            continuation();
        }
    }

    #endregion

    public void OnResponse(NetState ns, string base64Json)
    {
        // TODO: use spans
        var bytes = Convert.FromBase64String(base64Json);
        var response = Encoding.UTF8.GetString(bytes);
        if (typeof(TResponse) == typeof(string))
        {
            ResponseData = response as TResponse;
        }
        else
        {
            try
            {
                ResponseData = JsonSerializer.Deserialize<TResponse>(response, OutgoingZuluPackets.SerializerOptions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        IsCompleted = true;
        Continuation?.Invoke();
    }
}