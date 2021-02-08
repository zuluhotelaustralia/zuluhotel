using Server.Spells.Seventh;

namespace Server.Items
{
    public class EnergyFieldWand : BaseWand
    {
        [Constructible]
        public EnergyFieldWand() : base(WandEffect.EnergyField, 2, 10)
        {
        }

        [Constructible]
        public EnergyFieldWand(Serial serial) : base(serial)
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
            Cast(new EnergyFieldSpell(from, this));
        }
    }
}
