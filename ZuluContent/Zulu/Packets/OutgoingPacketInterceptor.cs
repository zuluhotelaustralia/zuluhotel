using System;
using System.Buffers;
using System.IO;
using System.Text;
using Server;
using Server.Items;
using Server.Network;
using static Server.Network.OutgoingMessagePackets;

namespace Scripts.Zulu.Packets
{
    public static class OutgoingPacketInterceptor
    {
        private static readonly bool RewriteOutgoingMessagesToAscii;
        
        static OutgoingPacketInterceptor()
        {
            RewriteOutgoingMessagesToAscii = 
                ServerConfiguration.GetOrUpdateSetting("outgoingPacketInterceptor.rewriteMessagesToAscii", true);
        }
        
        public static void Intercept(ReadOnlySpan<byte> input, CircularBuffer<byte> output, out int length)
        {
            switch (input[0])
            {
                case 0x1C:
                    break;
                case 0xBF:
                    if (RewriteOutgoingMessagesToAscii && input[4] == 0x10)
                    {
                        RewriteEquipmentInfo(input, output, out length);
                        return;
                    }
                    break;
                case 0xAE:
                    if (RewriteOutgoingMessagesToAscii)
                    {
                        RewriteUnicodeMessage(input, output, out length);
                        return;
                    }
                    break;
                case 0xC1:
                case 0xCC:
                    if (RewriteOutgoingMessagesToAscii)
                    {
                        RewriteMessageLocalized(input, output, out length);
                        return;
                    }
                    break;
            }

            length = NetworkCompression.Compress(input, output);
        }

        private static void RewriteEquipmentInfo(ReadOnlySpan<byte> input, CircularBuffer<byte> output, out int length)
        {
            var reader = new SpanReader(input);
            reader.Seek(3, SeekOrigin.Current);
            
            var sub = reader.ReadInt16();
            var serial = (Serial) reader.ReadUInt32();
            var label = reader.ReadInt32();

            var item = World.FindItem(serial);

            if ( !ZhConfig.Messaging.Cliloc.TryGetValue(label, out var text))
            {
                length = NetworkCompression.Compress(input, output);
                return;
            }

            var res = item switch
            {
                BaseClothing clothing => clothing.Resource,
                BaseWeapon weapon => weapon.Resource,
                BaseArmor armor => armor.Resource,
                BaseJewel jewel => jewel.Resource,
                _ => CraftResource.None
            };

            if (res != CraftResource.None)
                text = $"{CraftResources.GetName(res)} {text}";
            
            var buffer = stackalloc byte[GetMaxMessageLength(text)].InitializePacket();
            var pLength = CreateMessage(
                buffer,
                serial,
                item.ItemID,
                MessageType.Label,
                0,
                3,
                true,
                null,
                "",
                text
            );

            buffer = buffer[..pLength];
            length = NetworkCompression.Compress(buffer, output);
        }
        
        private static void RewriteUnicodeMessage(ReadOnlySpan<byte> input, CircularBuffer<byte> output, out int length)
        {
            var reader = new SpanReader(input);
            reader.Seek(3, SeekOrigin.Current);

            var serial = (Serial) reader.ReadUInt32();
            var graphic = reader.ReadInt16();
            var type = (MessageType) reader.ReadByte();
            var hue = reader.ReadInt16();
            var font = reader.ReadInt16();
            var lang = reader.ReadAscii(4);
            var name = reader.ReadAscii(30);
            var text = reader.ReadBigUni();
            
            var buffer = stackalloc byte[GetMaxMessageLength(text)].InitializePacket();
            var pLength = CreateMessage(
                buffer,
                serial,
                graphic,
                type,
                hue,
                font,
                true,
                null,
                name,
                text
            );

            buffer = buffer[..pLength];
            length = NetworkCompression.Compress(buffer, output);
        }
        
        private static void RewriteMessageLocalized(ReadOnlySpan<byte> input, CircularBuffer<byte> output, out int length)
        {
            var isAffix = input[0] == 0xCC;

            var reader = new SpanReader(input);
            reader.Seek(3, SeekOrigin.Current);
            var serial = (Serial)reader.ReadUInt32();
            var graphic = reader.ReadInt16();
            var type = (MessageType)reader.ReadByte();
            var hue = reader.ReadInt16();
            var font = reader.ReadInt16();
            var label = reader.ReadInt32();
            var flags = isAffix ? (AffixType) reader.ReadByte() : AffixType.System;
            var name = reader.ReadAscii(30);
            var affix = isAffix ? reader.ReadAscii() : string.Empty;
            var args = isAffix ? reader.ReadBigUni() : reader.ReadLittleUni();
            
            if (!ZhConfig.Messaging.Cliloc.ContainsKey(label))
            {
                length = NetworkCompression.Compress(input, output);
                return;
            }
            
            var text = ClilocList.Translate(label, args);
            
            if (isAffix)
            {
                text = flags switch
                {
                    AffixType.Append => $"{text}{affix}",
                    AffixType.Prepend => $"{affix}{text}",
                    _ => $"{text}{affix}"
                };

                if ((flags & AffixType.System) != 0)
                    type = MessageType.System;
            }

            var buffer = stackalloc byte[GetMaxMessageLength(text)].InitializePacket();
            var pLength = CreateMessage(
                buffer,
                serial,
                graphic,
                type,
                hue,
                font,
                true,
                null,
                name,
                text
            );

            buffer = buffer[..pLength];

            length = NetworkCompression.Compress(buffer, output);
        }
    }
}