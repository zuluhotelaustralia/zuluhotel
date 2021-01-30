using Server.Spells;

namespace Server.Items
{
    public class WraithsBreathScroll : NecroSpellScroll
    {
        [Constructible]
        public WraithsBreathScroll() : this(1)
        {
        }


        [Constructible]
        public WraithsBreathScroll(int amount) : base(SpellEntry.WraithBreath, 0x1F3F, amount)
        {
        }

        [Constructible]
        public WraithsBreathScroll(Serial serial) : base(serial)
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