using Server.Network;

namespace Server.Gumps;

public interface IJsonGump
{
    public void OnResponse(NetState ns, string base64Json);
}