using Server.Spells;

namespace Server.Items
{
    public class EarthBlessingScroll : EarthSpellScroll
    {
        [Constructible]
        public EarthBlessingScroll() : this(1)
        {
        }


        [Constructible]
        public EarthBlessingScroll(int amount) : base(SpellEntry.EarthsBlessing, 0x1F32, amount)
        {
        }

        [Constructible]
        public EarthBlessingScroll(Serial serial) : base(serial)
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