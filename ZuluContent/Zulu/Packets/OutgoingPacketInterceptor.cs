using System;
using System.Buffers;
using System.IO;
using System.Text;
using Server;
using Server.Network;
using static Server.Network.OutgoingMessagePackets;

namespace Scripts.Zulu.Packets
{
    public static class OutgoingPacketInterceptor
    {
        public static void Intercept(ReadOnlySpan<byte> input, CircularBuffer<byte> output, out int length)
        {
            var reader = new SpanReader(input);
            
            Span<byte> buffer = new byte[input.Length];
            var writer = new SpanWriter(buffer);
            
            //0x1C : 0xAE
            switch (reader.ReadByte())
            {
                case 0x1C:
                    Console.WriteLine("Sending ascii message packet");
                    break;
                case 0xA3:
                    Console.WriteLine("Sending unicode message packet");
                    break;
                case 0xC1:
                    Console.WriteLine("Sending CreateMessageLocalized message packet");
                    break;
                case 0xCC:
                    Console.WriteLine("Sending CreateMessageLocalizedAffix message packet");
                    break;
            }
            
            NetworkCompression.Compress(buffer, output, out length);
        }
        
        public static void DelocalizeMessage(ReadOnlySpan<byte> input, CircularBuffer<byte> output, out int length)
        {
            // Localized Message Package
            if (input[0] != 0xC1)
            {
                NetworkCompression.Compress(input, output, out length);
                return;
            }

            var reader = new SpanReader(input);
            reader.Seek(3, SeekOrigin.Current);
            var serial = (Serial)reader.ReadUInt32();
            var graphic = reader.ReadInt16();
            var type = (MessageType)reader.ReadByte();
            var hue = reader.ReadInt16();
            var font = reader.ReadInt16();
            var cliloc = reader.ReadInt32();
            var name = reader.ReadString(Encoding.ASCII, true, 30);
            var args = reader.ReadLittleUni();
            //
            // reader.ReadByte();
            // var serial = reader.ReadUInt32();
            // var graphic = reader.ReadInt16();
            // var type = (MessageType) reader.ReadByte();
            // var hue = reader.ReadInt16();
            // var font = reader.ReadInt16();
            // var number = reader.ReadInt32();
            // var flags = AffixType.System;
            // var name = reader.ReadAscii(30);
            // var affix = string.Empty;
            // var args = reader.ReadLittleUni();


            if (!ClilocList.Entries.TryGetValue(cliloc, out var text))
            {
                NetworkCompression.Compress(input, output, out length);
                return;
            }

            Span<byte> buffer = stackalloc byte[GetMaxMessageLength(text)].InitializePacket();
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

            buffer = buffer.SliceToLength(pLength);

            length = NetworkCompression.Compress(buffer, output);
        }
    }
}