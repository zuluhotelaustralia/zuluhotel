using System;
using System.Buffers;
using Server.Mobiles;
using Server.Network;

namespace Scripts.Zulu.Packets
{
    public static class OutgoingZuluPackets
    {
        public const int ZuluPlayerStatusMaxLength = 32;
        
        public static void SendZuluPlayerStatus(NetState ns, PlayerMobile player)
        {
            if (ns == null || player == null)
                return;

            var writer = new SpanWriter(stackalloc byte[ZuluPlayerStatusMaxLength]);
            writer.Write((byte)0xF9); // Packet ID
            writer.Write((byte)0x1); // Sub-command PlayerStatus
            writer.Write(player.Serial);
            writer.Write((ushort)0); // Hunger
            writer.Write((ushort)player.HealingBonus);
            writer.Write((ushort)player.MagicImmunity);
            writer.Write((ushort)player.MagicReflection);
            writer.Write((ushort)player.PhysicalResist);
            writer.Write((ushort)player.PoisonImmunity);
            writer.Write((ushort)player.FireResist);
            writer.Write((ushort)player.WaterResist);
            writer.Write((ushort)player.AirResist);
            writer.Write((ushort)player.EarthResist);
            writer.Write((ushort)player.NecroResist);
            writer.Write((ushort)0); // Criminal Timer Minutes
            writer.Write((ushort)0); // Murderer Timer Minutes
            
            ns.Send(writer.Span);
        }
    }
}