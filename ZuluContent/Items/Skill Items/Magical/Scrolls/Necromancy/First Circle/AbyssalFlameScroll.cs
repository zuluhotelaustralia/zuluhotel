using Server.Spells;

namespace Server.Items
{
    public class AbyssalFlameScroll : NecroSpellScroll
    {
        [Constructible]
        public AbyssalFlameScroll() : this(1)
        {
        }


        [Constructible]
        public AbyssalFlameScroll(int amount) : base(SpellEntry.AbyssalFlame, 0x1F3E, amount)
        {
        }

        [Constructible]
        public AbyssalFlameScroll(Serial serial) : base(serial)
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