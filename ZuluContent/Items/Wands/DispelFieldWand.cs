using Server.Spells.Fifth;

namespace Server.Items
{
    public class DispelFieldWand : BaseWand
    {
        [Constructible]
        public DispelFieldWand() : base(WandEffect.DispelField, 2, 10)
        {
        }

        [Constructible]
        public DispelFieldWand(Serial serial) : base(serial)
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
            Cast(new DispelFieldSpell(from, this));
        }
    }
}
