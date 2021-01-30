using Server.Spells;

namespace Server.Items
{
    public class KillScroll : NecroSpellScroll
    {
        [Constructible]
        public KillScroll() : this(1)
        {
        }


        [Constructible]
        public KillScroll(int amount) : base(SpellEntry.Kill, 0x1F35, amount)
        {
        }

        [Constructible]
        public KillScroll(Serial serial) : base(serial)
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