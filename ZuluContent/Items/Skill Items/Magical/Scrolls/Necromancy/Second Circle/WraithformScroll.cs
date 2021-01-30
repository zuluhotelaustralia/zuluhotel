using Server.Spells;

namespace Server.Items
{
    public class WraithformScroll : NecroSpellScroll
    {
        [Constructible]
        public WraithformScroll() : this(1)
        {
        }


        [Constructible]
        public WraithformScroll(int amount) : base(SpellEntry.WraithForm, 0x1F37, amount)
        {
        }

        [Constructible]
        public WraithformScroll(Serial serial) : base(serial)
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