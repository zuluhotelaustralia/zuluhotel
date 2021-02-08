using Server.Spells.Eighth;

namespace Server.Items
{
    public class EnergyVortexWand : BaseWand
    {
        [Constructible]
        public EnergyVortexWand() : base(WandEffect.EnergyVortex, 2, 10)
        {
        }

        [Constructible]
        public EnergyVortexWand(Serial serial) : base(serial)
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
            Cast(new EnergyVortexSpell(from, this));
        }
    }
}
