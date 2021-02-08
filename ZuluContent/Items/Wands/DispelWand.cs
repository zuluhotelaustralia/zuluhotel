using Server.Spells.Sixth;

namespace Server.Items
{
    public class DispelWand : BaseWand
    {
        [Constructible]
        public DispelWand() : base(WandEffect.Dispel, 2, 10)
        {
        }

        [Constructible]
        public DispelWand(Serial serial) : base(serial)
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
            Cast(new DispelSpell(from, this));
        }
    }
}
