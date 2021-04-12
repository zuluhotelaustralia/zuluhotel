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

        public virtual int Piece => 0;

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

    public class BaseShell : BaseShrine
    {
        [Constructible]
        public BaseShell(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public BaseShell(Serial serial) : base(serial)
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

    public class AquariaShell : BaseShell
    {
        public override int Piece => 1;
        public override string DefaultName => "Aquaria Shell 1";

        [Constructible]
        public AquariaShell() : base(0xFC4)
        {
            Hue = 0x504;
        }

        [Constructible]
        public AquariaShell(Serial serial) : base(serial)
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
    
    public class CapricorniaShell : BaseShell
    {
        public override int Piece => 2;
        public override string DefaultName => "Capricornia Shell 2";

        [Constructible]
        public CapricorniaShell() : base(0xFC5)
        {
            Hue = 0x519;
        }

        [Constructible]
        public CapricorniaShell(Serial serial) : base(serial)
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
    
    public class SeaNymphShell : BaseShell
    {
        public override int Piece => 3;
        public override string DefaultName => "Sea Nymph 3";

        [Constructible]
        public SeaNymphShell() : base(0xFC6)
        {
            Hue = 0x606;
        }

        [Constructible]
        public SeaNymphShell(Serial serial) : base(serial)
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
    
    public class NeptunesNautilusShell : BaseShell
    {
        public override int Piece => 4;
        public override string DefaultName => "Neptune's Nautilus 4";

        [Constructible]
        public NeptunesNautilusShell() : base(0xFC7)
        {
            Hue = 0x505;
        }

        [Constructible]
        public NeptunesNautilusShell(Serial serial) : base(serial)
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
    
    public class SeaShoreSandDollarShell : BaseShell
    {
        public override int Piece => 5;
        public override string DefaultName => "Sea Shore Sand Dollar 5";

        [Constructible]
        public SeaShoreSandDollarShell() : base(0xFC8)
        {
            Hue = 0x501;
        }

        [Constructible]
        public SeaShoreSandDollarShell(Serial serial) : base(serial)
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
    
    public class DiviniaShell : BaseShell
    {
        public override int Piece => 6;
        public override string DefaultName => "Divinia Shell 6";

        [Constructible]
        public DiviniaShell() : base(0xFC9)
        {
            Hue = 0x499;
        }

        [Constructible]
        public DiviniaShell(Serial serial) : base(serial)
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
    
    public class MermaidShell : BaseShell
    {
        public override int Piece => 7;
        public override string DefaultName => "Mermaid Shell 7";

        [Constructible]
        public MermaidShell() : base(0xFCA)
        {
            Hue = 0x48D;
        }

        [Constructible]
        public MermaidShell(Serial serial) : base(serial)
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
    
    public class OceanOdysseyShell : BaseShell
    {
        public override int Piece => 8;
        public override string DefaultName => "Ocean Odyssey 8";

        [Constructible]
        public OceanOdysseyShell() : base(0xFCB)
        {
            Hue = 0x489;
        }

        [Constructible]
        public OceanOdysseyShell(Serial serial) : base(serial)
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
    
    public class TalimariShell : BaseShell
    {
        public override int Piece => 9;
        public override string DefaultName => "Talimari 9";

        [Constructible]
        public TalimariShell() : base(0xFCC)
        {
            Hue = 0x496;
        }

        [Constructible]
        public TalimariShell(Serial serial) : base(serial)
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
    
}