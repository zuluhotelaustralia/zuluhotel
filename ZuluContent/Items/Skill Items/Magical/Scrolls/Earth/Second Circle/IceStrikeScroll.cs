using Server.Spells;

namespace Server.Items
{
    public class IceStrikeScroll : EarthSpellScroll
    {
        [Constructible]
        public IceStrikeScroll() : this(1)
        {
        }


        [Constructible]
        public IceStrikeScroll(int amount) : base(SpellEntry.IceStrike, 0x1F32, amount)
        {
        }

        [Constructible]
        public IceStrikeScroll(Serial serial) : base(serial)
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