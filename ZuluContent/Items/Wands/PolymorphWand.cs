using Server.Spells.Seventh;

namespace Server.Items
{
    public class PolymorphWand : BaseWand
    {
        [Constructible]
        public PolymorphWand() : base(WandEffect.Polymorph, 2, 10)
        {
        }

        [Constructible]
        public PolymorphWand(Serial serial) : base(serial)
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
            Cast(new PolymorphSpell(from, this));
        }
    }
}
