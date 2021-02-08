using Server.Spells.Third;

namespace Server.Items
{
    public class PoisonWand : BaseWand
    {
        [Constructible]
        public PoisonWand() : base(WandEffect.Poison, 5, 30)
        {
        }

        [Constructible]
        public PoisonWand(Serial serial) : base(serial)
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
            Cast(new PoisonSpell(from, this));
        }
    }
}
