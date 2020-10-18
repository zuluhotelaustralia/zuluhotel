using Server.Spells;

namespace Server.Items
{
    public class ClumsyScroll : SpellScroll
    {
        [Constructible]
        public ClumsyScroll() : this(1)
        {
        }


        [Constructible]
        public ClumsyScroll(int amount) : base(SpellEntry.Clumsy, 0x1F2E, amount)
        {
        }

        [Constructible]
        public ClumsyScroll(Serial serial) : base(serial)
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