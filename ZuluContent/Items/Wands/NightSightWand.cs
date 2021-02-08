using Server.Spells.First;

namespace Server.Items
{
    public class NightSightWand : BaseWand
    {

        [Constructible]
        public NightSightWand() : base(WandEffect.NightSight, 5, 30)
        {
        }

        [Constructible]
        public NightSightWand(Serial serial) : base(serial)
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
            Cast(new NightSightSpell(from, this));
        }
    }
}
