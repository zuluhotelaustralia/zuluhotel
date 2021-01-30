using Server.Spells;

namespace Server.Items
{
    public class DecayingRayScroll : NecroSpellScroll
    {
        [Constructible]
        public DecayingRayScroll() : this(1)
        {
        }


        [Constructible]
        public DecayingRayScroll(int amount) : base(SpellEntry.DecayingRay, 0x1F30, amount)
        {
        }

        [Constructible]
        public DecayingRayScroll(Serial serial) : base(serial)
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