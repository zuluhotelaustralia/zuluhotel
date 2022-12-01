using System;
using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Server;
using Server.Exceptions;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Packets
{
    public static class OutgoingZuluPackets
    {
        private const int ZuluPlayerStatusMaxLength = 38;
        private const string ZuluClientSuffix = "zuluhotel";

        public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReadCommentHandling = JsonCommentHandling.Skip,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

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

        private static bool IsZuluClient(NetState ns) =>
            ns?.Mobile != null && ns.Version.SourceString.InsensitiveContains(ZuluClientSuffix);

        public static void SendZuluPlayerStatus(NetState ns, PlayerMobile player)
        {
            WebPlayerStatusGump.Update(player);
        }
    }
}