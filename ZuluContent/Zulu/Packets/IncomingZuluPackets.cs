using System;
using System.Text;
using Server;
using Server.Guilds;
using Server.Gumps;
using Server.Network;

namespace Scripts.Zulu.Packets;

public static class IncomingZuluPackets
{
    private static readonly PacketHandler[] Handlers = new PacketHandler[0x100];

    public static void Configure()
    {
        IncomingPackets.Register(0xF9, 0, true, DecodeZuluPacket);
        Register(0x02, true, HandleJsGumpResponse);
    }

    private static void Register(int packetId, bool inGame, OnPacketReceive onReceive)
    {
        Handlers[packetId] = new PacketHandler(packetId, 0, inGame, onReceive);
    }

    private static PacketHandler GetHandler(int packetId) =>
        packetId >= 0 && packetId < Handlers.Length ? Handlers[packetId] : null;

    private static void DecodeZuluPacket(NetState state, CircularBufferReader reader, ref int packetLength)
    {
        int packetId = reader.ReadByte();

        var ph = GetHandler(packetId);

        if (ph == null)
            return;

        switch (ph.Ingame)
        {
            case true when state.Mobile == null:
                state.Disconnect($"Sent in-game packet (0xBFx{packetId:X2}) before having been attached to a mobile");
                break;
            case true when state.Mobile.Deleted:
                state.Disconnect(string.Empty);
                break;
            default:
                ph.OnReceive(state, reader, ref packetLength);
                break;
        }
    }

    private static void HandleJsGumpResponse(NetState state, CircularBufferReader reader, ref int packetLength)
    {
        var gumpId = (Serial)reader.ReadUInt32();
        var typeId = reader.ReadUInt32();
        var len = reader.ReadUInt16();
        var json = reader.ReadUTF8Safe(len);

        foreach (var g in state.Gumps)
        {
            if (g.Serial != gumpId || g.TypeID != typeId)
                continue;

            if (g is IJsonGump jsonGump) 
                jsonGump.OnResponse(state, json);
        }
    }

}
