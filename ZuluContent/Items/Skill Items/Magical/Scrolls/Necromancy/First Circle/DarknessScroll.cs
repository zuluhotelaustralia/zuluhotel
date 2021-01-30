using Server.Spells;

namespace Server.Items
{
    public class DarknessScroll : NecroSpellScroll
    {
        [Constructible]
        public DarknessScroll() : this(1)
        {
        }


        [Constructible]
        public DarknessScroll(int amount) : base(SpellEntry.Darkness, 0x1F3D, amount)
        {
        }

        [Constructible]
        public DarknessScroll(Serial serial) : base(serial)
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