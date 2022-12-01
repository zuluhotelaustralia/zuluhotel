using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Server.Network;

namespace Server.Gumps;

public abstract class WebGump : Gump
{
    private readonly string m_Type;
    private Action<string> m_OnResponse;

    public bool Open { get; private set; } = true;

    public static readonly JsonSerializerOptions SerializerOptions = new ()
    {
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        ReadCommentHandling = JsonCommentHandling.Skip,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    public WebGump(string type) : base(default, default)
    {
        m_Type = type;
        Disposable = false;
        Closable = false;
    }

    protected virtual void Prepare<TRequest>(TRequest requestData, int maxResponseSegments) where TRequest : class
    {
        Entries.Clear();
        Entries.Add(new GumpLabel(0, 0, 0, $"WebGump {m_Type}"));
        Entries.Add(new GumpButton(0, 0, 0, 0, 1000));
        var json = JsonSerializer.Serialize(requestData, SerializerOptions);
        Entries.Add(new GumpHtml(0, 0, 0, 0, json, false, false));
        
        for (var i = 0; i < maxResponseSegments; i++) 
            Entries.Add(new GumpTextEntry(0, 0, 0, 0, 0, 0, string.Empty));
    }

    public WebGumpRequest<TRequest, TResponse> SendAsync<TRequest, TResponse>(NetState owner, TRequest requestData, int maxResponseSegments = 256) where TRequest : class where TResponse : class
    {
        Prepare(requestData, maxResponseSegments);
        
        var request = new WebGumpRequest<TRequest, TResponse>(this, requestData, owner);
        m_OnResponse = request.SetResponseBody;
        return request;
    }

    public void Send<TRequest>(NetState owner, TRequest requestData) where TRequest : class
    {
        owner.RemoveGump(this);
        Prepare(requestData, 0);
        SendTo(owner);
    }

    public override void OnServerClose(NetState owner)
    {
        base.OnServerClose(owner);
        Open = false;
        m_OnResponse?.Invoke(null);
    }

    public void Close(NetState owner)
    {
        if (!Open)
            return;
        
        Open = false;
        owner.SendCloseGump(TypeID, 0);
        owner.RemoveGump(this);
        m_OnResponse?.Invoke(null);
    }

    public override void OnResponse(NetState sender, RelayInfo info)
    {
        if (info.ButtonID == 0)
        {
            Close(sender);
            return;
        }
        
        var sb = new StringBuilder();

        foreach (var entry in info.TextEntries) 
            sb.Append(entry.Text);

        m_OnResponse?.Invoke(sb.ToString());
    }
}