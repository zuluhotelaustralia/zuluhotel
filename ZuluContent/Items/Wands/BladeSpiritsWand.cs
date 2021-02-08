using Server.Spells.Fifth;

namespace Server.Items
{
    public class BladeSpiritsWand : BaseWand
    {
        [Constructible]
        public BladeSpiritsWand() : base(WandEffect.BladeSpirits, 2, 10)
        {
        }

        [Constructible]
        public BladeSpiritsWand(Serial serial) : base(serial)
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
            Cast(new BladeSpiritsSpell(from, this));
        }
    }
}
