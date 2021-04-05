namespace Server.Items
{
    public class AlchemicalSymbol1 : Item
    {
        [Constructible]
        public AlchemicalSymbol1() : base(0x181D)
        {
            Weight = 1.0;
        }

        [Constructible]
        public AlchemicalSymbol1(Serial serial) : base(serial)
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
    
    public class AlchemicalSymbol2 : Item
    {
        [Constructible]
        public AlchemicalSymbol2() : base(0x181E)
        {
            Weight = 1.0;
        }

        [Constructible]
        public AlchemicalSymbol2(Serial serial) : base(serial)
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
    
    public class AlchemicalSymbol3 : Item
    {
        [Constructible]
        public AlchemicalSymbol3() : base(0x181F)
        {
            Weight = 1.0;
        }

        [Constructible]
        public AlchemicalSymbol3(Serial serial) : base(serial)
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
    
    public class AlchemicalSymbol4 : Item
    {
        [Constructible]
        public AlchemicalSymbol4() : base(0x1821)
        {
            Weight = 1.0;
        }

        [Constructible]
        public AlchemicalSymbol4(Serial serial) : base(serial)
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
    
    public class AlchemicalSymbol5 : Item
    {
        [Constructible]
        public AlchemicalSymbol5() : base(0x1822)
        {
            Weight = 1.0;
        }

        [Constructible]
        public AlchemicalSymbol5(Serial serial) : base(serial)
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
    
    public class AlchemicalSymbol6 : Item
    {
        [Constructible]
        public AlchemicalSymbol6() : base(0x1823)
        {
            Weight = 1.0;
        }

        [Constructible]
        public AlchemicalSymbol6(Serial serial) : base(serial)
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
    
    public class AlchemicalSymbol7 : Item
    {
        [Constructible]
        public AlchemicalSymbol7() : base(0x1824)
        {
            Weight = 1.0;
        }

        [Constructible]
        public AlchemicalSymbol7(Serial serial) : base(serial)
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
    
    public class AlchemicalSymbol8 : Item
    {
        [Constructible]
        public AlchemicalSymbol8() : base(0x1825)
        {
            Weight = 1.0;
        }

        [Constructible]
        public AlchemicalSymbol8(Serial serial) : base(serial)
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
    
    public class AlchemicalSymbol9 : Item
    {
        [Constructible]
        public AlchemicalSymbol9() : base(0x1826)
        {
            Weight = 1.0;
        }

        [Constructible]
        public AlchemicalSymbol9(Serial serial) : base(serial)
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
    
    public class AlchemicalSymbol10 : Item
    {
        [Constructible]
        public AlchemicalSymbol10() : base(0x1827)
        {
            Weight = 1.0;
        }

        [Constructible]
        public AlchemicalSymbol10(Serial serial) : base(serial)
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
    
    public class AlchemicalSymbol11 : Item
    {
        [Constructible]
        public AlchemicalSymbol11() : base(0x1828)
        {
            Weight = 1.0;
        }

        [Constructible]
        public AlchemicalSymbol11(Serial serial) : base(serial)
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