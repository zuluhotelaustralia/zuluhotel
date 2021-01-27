using Server.Spells;

namespace Server.Items
{
    public class EarthPortalScroll : EarthSpellScroll
    {
        [Constructible]
        public EarthPortalScroll() : this(1)
        {
        }


        [Constructible]
        public EarthPortalScroll(int amount) : base(SpellEntry.EarthPortal, 0x1F30, amount)
        {
        }

        [Constructible]
        public EarthPortalScroll(Serial serial) : base(serial)
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