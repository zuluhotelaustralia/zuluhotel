using System;
using Scripts.Zulu.Packets;
using Server.Network;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Items
{
    public abstract class BaseShrine : Item
    {
        public override double DefaultWeight { get; } = 1.0;

        public int Piece { get; set; }

        public BaseShrine(int itemID) : base(itemID)
        {
        }

        public BaseShrine(Serial serial) : base(serial)
        {
        }
        
        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class Shell : BaseShrine
    {
        private static string[] Names = { "Aquaria Shell 1", "Capricornia Shell 2", "Sea Nymph 3", "Neptune's Nautilus 4", "Sea Shore Sand Dollar 5", "Divinia Shell 6", "Mermaid Shell 7", "Ocean Odyssey 8", "Talimari 9" };

        private static int[] ItemIDs = { 0xFC4, 0xFC5, 0xFC6, 0xFC7, 0xFC8, 0xFC9, 0xFCA, 0xFCB, 0xFCC  };

        private static int[] Hues = { 0x504, 0x519, 0x606, 0x505, 0x501, 0x499, 0x480, 0x489, 0x496 };
        
        [Constructible]
        public Shell(int piece) : base(ItemIDs[piece])
        {
            Piece = piece;
            Name = Names[piece];
            Hue = Hues[piece];
        }

        [Constructible]
        public Shell(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
            
            writer.WriteEncodedInt(Piece);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            Piece = reader.ReadEncodedInt();
        }
    }
}