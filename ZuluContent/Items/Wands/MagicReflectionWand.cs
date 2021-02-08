using Server.Spells.Fifth;

namespace Server.Items
{
    public class MagicReflectionWand : BaseWand
    {
        [Constructible]
        public MagicReflectionWand() : base(WandEffect.MagicReflection, 2, 10)
        {
        }

        [Constructible]
        public MagicReflectionWand(Serial serial) : base(serial)
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
            Cast(new MagicReflectSpell(from, this));
        }
    }
}
