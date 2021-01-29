using Server.Spells;

namespace Server.Items
{
    public class SacrificeScroll : NecroSpellScroll
    {
        [Constructible]
        public SacrificeScroll() : this(1)
        {
        }


        [Constructible]
        public SacrificeScroll(int amount) : base(SpellEntry.Sacrifice, 0x1F33, amount)
        {
        }

        [Constructible]
        public SacrificeScroll(Serial serial) : base(serial)
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