using Server.Spells;

namespace Server.Items
{
    public class AbyssalFlame : NecroSpellScroll
    {
        [Constructible]
        public AbyssalFlame() : this(1)
        {
        }


        [Constructible]
        public AbyssalFlame(int amount) : base(SpellEntry.AbyssalFlame, 0x1F3E, amount)
        {
        }

        [Constructible]
        public AbyssalFlame(Serial serial) : base(serial)
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