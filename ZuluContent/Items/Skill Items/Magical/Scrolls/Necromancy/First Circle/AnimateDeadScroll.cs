using Server.Spells;

namespace Server.Items
{
    public class AnimateDeadScroll : NecroSpellScroll
    {
        [Constructible]
        public AnimateDeadScroll() : this(1)
        {
        }


        [Constructible]
        public AnimateDeadScroll(int amount) : base(SpellEntry.AnimateDead, 0x1F31, amount)
        {
        }

        [Constructible]
        public AnimateDeadScroll(Serial serial) : base(serial)
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