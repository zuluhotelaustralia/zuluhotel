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
        private const int JsGumpOpenMaxLength = 10 * 1024; // 10KB, theoretically could be up to < 64KB 
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
            ns?.Mobile != null && ns.Version?.SourceString != null && ns.Version.SourceString.InsensitiveContains(ZuluClientSuffix);

        public static void SendZuluPlayerStatus(NetState ns, PlayerMobile player)
        {
            if (!IsZuluClient(ns))
                return;

            int minDamage = 0, maxDamage = 0;
            player.Weapon?.GetStatusDamage(player, out minDamage, out maxDamage);

            var writer = new SpanWriter(stackalloc byte[ZuluPlayerStatusMaxLength]);
            writer.Write((byte)0xF9); // Packet ID
            writer.Seek(2, SeekOrigin.Current);
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
            writer.WritePacketLength();

            ns.Send(writer.Span);
        }

        public static void SendJsGumpOpen(NetState ns, Gump gump, object data)
        {
            // if (!IsZuluClient(ns))
            //     return;
            
            const int headerLength = 14;
            
            byte[] outputBytes = null;
            try
            {
                var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(data, SerializerOptions);
                var jsonLength = Base64.GetMaxEncodedToUtf8Length(jsonBytes.Length);
                
                if (jsonLength > JsGumpOpenMaxLength)
                {
                    var exception = new InvalidGumpResponseException($"SendJsGumpOpen json payload is too long ({jsonLength})");
                    exception.SetStackTrace(new StackTrace());
                    NetState.TraceException(exception);
                    return;
                }

                outputBytes = ArrayPool<byte>.Shared.Rent(jsonLength);
                Base64.EncodeToUtf8(jsonBytes, outputBytes, out _, out var base64Length);

                var writer = new SpanWriter(headerLength + base64Length, true);
                writer.Write((byte)0xF9); // Packet ID
                writer.Seek(2, SeekOrigin.Current);
                writer.Write((byte)0x2); // Sub-command JsGumpOpen
                writer.Write(gump.Serial);
                writer.Write(gump.TypeID);
                writer.Write((ushort)jsonLength);
                writer.Write(outputBytes);
                writer.WritePacketLength();
                
                ns.Send(writer.Span);
                writer.Dispose();
            }
            finally
            {
                if (outputBytes != null)
                    ArrayPool<byte>.Shared.Return(outputBytes);
            }
        }
    }
}