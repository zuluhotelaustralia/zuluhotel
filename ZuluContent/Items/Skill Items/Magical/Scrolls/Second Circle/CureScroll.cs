using Server.Spells;

namespace Server.Items
{
    public class CureScroll : SpellScroll
    {
        [Constructible]
        public CureScroll() : this(1)
        {
        }


        [Constructible]
        public CureScroll(int amount) : base(SpellEntry.Cure, 0x1F37, amount)
        {
        }

        [Constructible]
        public CureScroll(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}