using Server.Spells.Eighth;

namespace Server.Items
{
    public class AirElementalWand : BaseWand
    {
        [Constructible]
        public AirElementalWand() : base(WandEffect.AirElemental, 2, 10)
        {
        }

        [Constructible]
        public AirElementalWand(Serial serial) : base(serial)
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
            Cast(new AirElementalSpell(from, this));
        }
    }
}
