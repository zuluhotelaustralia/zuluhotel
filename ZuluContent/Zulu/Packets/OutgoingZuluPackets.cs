using System;
using System.Buffers;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Scripts.Zulu.Packets
{
    public static class OutgoingZuluPackets
    {
        private const int ZuluPlayerStatusMaxLength = 32;
        private const string ZuluClientSuffix = "zuluhotel";
        
        public static void Initialize()
        {
            static void ZuluPlayerStatusEventHandler(Mobile mobile)
            {
                if (mobile is PlayerMobile player) 
                    Timer.DelayCall(TimeSpan.Zero, SendZuluPlayerStatus, player.NetState, player);
            }

            EventSink.Connected += ZuluPlayerStatusEventHandler;
            EventSink.PlayerDeath += ZuluPlayerStatusEventHandler;
            EventSink.Login += ZuluPlayerStatusEventHandler;
        }
        
        public static void SendZuluPlayerStatus(NetState ns, PlayerMobile player)
        {
            if (ns == null || player == null || !ns.Version.SourceString.InsensitiveContains(ZuluClientSuffix))
                return;

            var writer = new SpanWriter(stackalloc byte[ZuluPlayerStatusMaxLength]);
            writer.Write((byte)0xF9); // Packet ID
            writer.Write((byte)0x1); // Sub-command PlayerStatus
            writer.Write(player.Serial);
            writer.Write((short)0); // Hunger
            writer.Write((short)player.HealingBonus);
            writer.Write((short)player.MagicImmunity);
            writer.Write((short)player.MagicReflection);
            writer.Write((short)player.PhysicalResist);
            writer.Write((short)player.PoisonImmunity);
            writer.Write((short)player.FireResist);
            writer.Write((short)player.WaterResist);
            writer.Write((short)player.AirResist);
            writer.Write((short)player.EarthResist);
            writer.Write((short)player.NecroResist);
            writer.Write((short)0); // Criminal Timer Minutes
            writer.Write((short)0); // Murderer Timer Minutes
            
            ns.Send(writer.Span);
        }
    }
}