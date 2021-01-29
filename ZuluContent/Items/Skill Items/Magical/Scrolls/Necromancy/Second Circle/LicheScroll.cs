using Server.Spells;

namespace Server.Items
{
    public class LicheScroll : NecroSpellScroll
    {
        [Constructible]
        public LicheScroll() : this(1)
        {
        }


        [Constructible]
        public LicheScroll(int amount) : base(SpellEntry.LicheForm, 0x1F3A, amount)
        {
        }

        [Constructible]
        public LicheScroll(Serial serial) : base(serial)
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