using Server.Spells;

namespace Server.Items
{
    public class PlagueScroll : NecroSpellScroll
    {
        [Constructible]
        public PlagueScroll() : this(1)
        {
        }


        [Constructible]
        public PlagueScroll(int amount) : base(SpellEntry.Plague, 0x1F3B, amount)
        {
        }

        [Constructible]
        public PlagueScroll(Serial serial) : base(serial)
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