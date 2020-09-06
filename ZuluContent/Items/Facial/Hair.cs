using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Items
{
    public abstract class Hair : Item
    {
        public static readonly IReadOnlyDictionary<int, Type> HairById = new Dictionary<int, Type>
        {
            {0x203B, typeof(ShortHair)},
            {0x203C, typeof(LongHair)},
            {0x203D, typeof(PonyTail)},
            {0x2044, typeof(Mohawk)},
            {0x2045, typeof(PageboyHair)},
            {0x2046, typeof(BunsHair)},
            {0x2047, typeof(Afro)},
            {0x2048, typeof(ReceedingHair)},
            {0x2049, typeof(TwoPigTails)},
            {0x204A, typeof(KrisnaHair)},
        };

        public static readonly List<int> MaleHairTypeIds =
            new List<int>(HairById.Keys).Except(new[] {0x2046}).ToList();

        public static readonly List<int> FemaleHairTypeIds =
            new List<int>(HairById.Keys);

        public static Hair GetRandomHair(bool female, int hairHue = -1)
        {
            var hairTypes = female ? FemaleHairTypeIds : MaleHairTypeIds;
            var id = hairTypes[Utility.Random(hairTypes.Count)];

            return CreateById(id, hairHue);
        }

        public static Hair CreateById(int id, int hue = -1)
        {
            if (hue == -1)
                hue = Utility.Random(1102, 48);;

            if (HairById.TryGetValue(id, out var hairType))
                return (Hair) Activator.CreateInstance(hairType, id, hue);

            return null;
        }

        protected Hair(int itemId) : this(itemId, 0)
        {
        }

        protected Hair(int itemId, int hue) : base(itemId)
        {
            LootType = LootType.Blessed;
            Layer = Layer.Hair;
            Hue = hue;
        }

        public Hair(Serial serial) : base(serial)
        {
        }

        public override bool DisplayLootType
        {
            get { return false; }
        }

        public override bool VerifyMove(Mobile from)
        {
            return (from.AccessLevel >= AccessLevel.GameMaster);
        }

        public override DeathMoveResult OnParentDeath(Mobile parent)
        {
            //			Dupe( Amount );

            parent.HairItemID = this.ItemID;
            parent.HairHue = this.Hue;

            return DeathMoveResult.MoveToCorpse;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            LootType = LootType.Blessed;

            int version = reader.ReadInt();
        }
    }

    public class GenericHair : Hair
    {
        private GenericHair(int itemId)
            : this(itemId, 0)
        {
        }


        private GenericHair(int itemId, int hue)
            : base(itemId, hue)
        {
        }

        public GenericHair(Serial serial)
            : base(serial)
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

    public class Mohawk : Hair
    {
        private Mohawk()
            : this(0)
        {
        }


        public Mohawk(int hue)
            : base(0x2044, hue)
        {
        }

        public Mohawk(Serial serial)
            : base(serial)
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

    public class PageboyHair : Hair
    {
        private PageboyHair()
            : this(0)
        {
        }


        public PageboyHair(int hue)
            : base(0x2045, hue)
        {
        }

        public PageboyHair(Serial serial)
            : base(serial)
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

    public class BunsHair : Hair
    {
        private BunsHair()
            : this(0)
        {
        }


        public BunsHair(int hue)
            : base(0x2046, hue)
        {
        }

        public BunsHair(Serial serial)
            : base(serial)
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

    public class LongHair : Hair
    {
        private LongHair()
            : this(0)
        {
        }


        public LongHair(int hue)
            : base(0x203C, hue)
        {
        }

        public LongHair(Serial serial)
            : base(serial)
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

    public class ShortHair : Hair
    {
        private ShortHair()
            : this(0)
        {
        }


        public ShortHair(int hue)
            : base(0x203B, hue)
        {
        }

        public ShortHair(Serial serial)
            : base(serial)
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

    public class PonyTail : Hair
    {
        private PonyTail()
            : this(0)
        {
        }


        public PonyTail(int hue)
            : base(0x203D, hue)
        {
        }

        public PonyTail(Serial serial)
            : base(serial)
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

    public class Afro : Hair
    {
        private Afro()
            : this(0)
        {
        }


        public Afro(int hue)
            : base(0x2047, hue)
        {
        }

        public Afro(Serial serial)
            : base(serial)
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

    public class ReceedingHair : Hair
    {
        private ReceedingHair()
            : this(0)
        {
        }


        public ReceedingHair(int hue)
            : base(0x2048, hue)
        {
        }

        public ReceedingHair(Serial serial)
            : base(serial)
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

    public class TwoPigTails : Hair
    {
        private TwoPigTails()
            : this(0)
        {
        }


        public TwoPigTails(int hue)
            : base(0x2049, hue)
        {
        }

        public TwoPigTails(Serial serial)
            : base(serial)
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

    public class KrisnaHair : Hair
    {
        private KrisnaHair()
            : this(0)
        {
        }


        public KrisnaHair(int hue)
            : base(0x204A, hue)
        {
        }

        public KrisnaHair(Serial serial)
            : base(serial)
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