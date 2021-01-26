using Server.Spells;

namespace Server.Items
{
    public class StormSpiritScroll : EarthSpellScroll
    {
        [Constructible]
        public StormSpiritScroll() : this(1)
        {
        }


        [Constructible]
        public StormSpiritScroll(int amount) : base(SpellEntry.StormSpirit, 0x1F32, amount)
        {
        }

        [Constructible]
        public StormSpiritScroll(Serial serial) : base(serial)
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