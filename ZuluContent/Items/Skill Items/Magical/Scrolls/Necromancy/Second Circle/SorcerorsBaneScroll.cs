using Server.Spells;

namespace Server.Items
{
    public class SorcerorsBaneScroll : NecroSpellScroll
    {
        [Constructible]
        public SorcerorsBaneScroll() : this(1)
        {
        }


        [Constructible]
        public SorcerorsBaneScroll(int amount) : base(SpellEntry.SorcerorsBane, 0x1F36, amount)
        {
        }

        [Constructible]
        public SorcerorsBaneScroll(Serial serial) : base(serial)
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