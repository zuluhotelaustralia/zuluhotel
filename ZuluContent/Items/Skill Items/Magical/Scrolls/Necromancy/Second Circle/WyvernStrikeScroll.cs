using Server.Spells;

namespace Server.Items
{
    public class WyvernStrikeScroll : NecroSpellScroll
    {
        [Constructible]
        public WyvernStrikeScroll() : this(1)
        {
        }


        [Constructible]
        public WyvernStrikeScroll(int amount) : base(SpellEntry.WyvernStrike, 0x1F3C, amount)
        {
        }

        [Constructible]
        public WyvernStrikeScroll(Serial serial) : base(serial)
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