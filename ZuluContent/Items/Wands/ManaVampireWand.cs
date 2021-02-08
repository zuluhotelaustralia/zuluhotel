using Server.Spells.Seventh;

namespace Server.Items
{
    public class ManaVampireWand : BaseWand
    {
        [Constructible]
        public ManaVampireWand() : base(WandEffect.ManaVampire, 2, 10)
        {
        }

        [Constructible]
        public ManaVampireWand(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void OnWandUse(Mobile from)
        {
            Cast(new ManaVampireSpell(from, this));
        }
    }
}
