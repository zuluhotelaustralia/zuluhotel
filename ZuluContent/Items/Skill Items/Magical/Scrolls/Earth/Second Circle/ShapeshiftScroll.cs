using Server.Spells;

namespace Server.Items
{
    public class ShapeshiftScroll : EarthSpellScroll
    {
        [Constructible]
        public ShapeshiftScroll() : this(1)
        {
        }


        [Constructible]
        public ShapeshiftScroll(int amount) : base(SpellEntry.Shapeshift, 0x1F32, amount)
        {
        }

        [Constructible]
        public ShapeshiftScroll(Serial serial) : base(serial)
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