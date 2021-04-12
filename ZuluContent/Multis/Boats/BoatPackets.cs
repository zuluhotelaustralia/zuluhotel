using System;
using System.Buffers;
using System.IO;
using Server.Network;

namespace Server.Multis.Boats
{
    public static class BoatPackets
    {
        public static void SendMoveBoatHS(this NetState ns, Mobile beholder, BaseBoat boat,
            Direction d, int speed, BaseBoat.MovingEntitiesEnumerable ents, int xOffset, int yOffset)
        {
            if (ns?.HighSeas != true)
            {
                return;
            }

            var minLength = 68; // 18 + 5 * 10
            var writer = new SpanWriter(stackalloc byte[minLength], true);
            writer.Write((byte)0xF6); // Packet ID
            writer.Seek(2, SeekOrigin.Current);

            writer.Write(boat.Serial);
            writer.Write((byte)speed);
            writer.Write((byte)d);
            writer.Write((byte)boat.Facing);
            writer.Write((short)(boat.X + xOffset));
            writer.Write((short)(boat.Y + yOffset));
            writer.Write((short)boat.Z);
            writer.Seek(2, SeekOrigin.Current); // count

            var count = 0;

            foreach (var ent in ents)
            {
                // If we assume that the entities list contains everything a player can see,
                // then this can be removed and the packet can be written once and copied to improve performance
                if (!beholder.CanSee(ent))
                {
                    continue;
                }

                writer.Write(ent.Serial);
                writer.Write((short)(ent.X + xOffset));
                writer.Write((short)(ent.Y + yOffset));
                writer.Write((short)ent.Z);
                ++count;
            }

            writer.Seek(16, SeekOrigin.Begin);
            writer.Write((short)count);
            writer.WritePacketLength();

            ns.Send(writer.Span);
        }

        public static void SendDisplayBoatHS(this NetState ns, Mobile beholder, BaseBoat boat)
        {
            if (ns?.HighSeas != true)
            {
                return;
            }

            var minLength = PacketContainerBuilder.MinPacketLength
                            + OutgoingEntityPackets.MaxWorldEntityPacketLength
                            * 5; // Minimum of boat, hold, planks, and the player

            using var builder = new PacketContainerBuilder(stackalloc byte[minLength]);

            Span<byte> buffer = builder.GetSpan(OutgoingEntityPackets.MaxWorldEntityPacketLength);

            foreach (var entity in boat.GetMovingEntities(true))
            {
                if (!beholder.CanSee(entity))
                {
                    continue;
                }

                buffer.InitializePacket();
                var bytesWritten = OutgoingEntityPackets.CreateWorldEntity(buffer, entity, ns.IsSAClient, true);
                builder.Advance(bytesWritten);
            }

            ns.Send(builder.Finalize());
        }
    }
}