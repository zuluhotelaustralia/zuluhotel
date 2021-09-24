using System;
using System.Buffers;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Packets
{
    public static class OutgoingZuluPackets
    {
        private const int ZuluPlayerStatusMaxLength = 36;
        private const string ZuluClientSuffix = "zuluhotel";
        
        public static void Initialize()
        {
            static void ZuluPlayerStatusEventHandler(Mobile mobile)
            {
                if (mobile is PlayerMobile player) 
                    Timer.StartTimer(() => SendZuluPlayerStatus(player.NetState, player));
            }
            
            static void ZuluPlayerHungerEventHandler(Mobile mobile, int oldHunger)
            {
                if (mobile is PlayerMobile player) 
                    Timer.StartTimer(() => SendZuluPlayerStatus(player.NetState, player));
            }

            EventSink.PlayerDeath += ZuluPlayerStatusEventHandler;
            EventSink.Login += ZuluPlayerStatusEventHandler;
            EventSink.HungerChanged += ZuluPlayerHungerEventHandler;

            static void OnBuffManagerAddRemove(Mobile mobile, IBuff buff)
            {
                if (buff is PoisonImmunity && mobile is PlayerMobile player)
                    Timer.StartTimer(() => SendZuluPlayerStatus(player.NetState, player));
            }
            
            BuffManager.OnAddBuff += OnBuffManagerAddRemove;
            BuffManager.OnRemoveBuff += OnBuffManagerAddRemove;

        }

        public static void SendZuluPlayerStatus(NetState ns, PlayerMobile player)
        {
            if (ns == null || player == null || !ns.Version.SourceString.InsensitiveContains(ZuluClientSuffix))
                return;

            int minDamage = 0, maxDamage = 0;
            player.Weapon?.GetStatusDamage(player, out minDamage, out maxDamage);

            var writer = new SpanWriter(stackalloc byte[ZuluPlayerStatusMaxLength]);
            writer.Write((byte)0xF9); // Packet ID
            writer.Write((byte)0x1); // Sub-command PlayerStatus
            writer.Write(player.Serial);
            writer.Write((short)player.Hunger);
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
            writer.Write((short)player.ShortTermMurders);
            writer.Write((short)player.Kills);
            writer.Write((short)minDamage);
            writer.Write((short)maxDamage);
            
            ns.Send(writer.Span);
        }
    }
}