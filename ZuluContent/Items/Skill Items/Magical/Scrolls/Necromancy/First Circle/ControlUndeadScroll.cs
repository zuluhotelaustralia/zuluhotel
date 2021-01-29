using Server.Spells;

namespace Server.Items
{
    public class ControlUndeadScroll : NecroSpellScroll
    {
        [Constructible]
        public ControlUndeadScroll() : this(1)
        {
        }


        [Constructible]
        public ControlUndeadScroll(int amount) : base(SpellEntry.ControlUndead, 0x1F32, amount)
        {
        }

        [Constructible]
        public ControlUndeadScroll(Serial serial) : base(serial)
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