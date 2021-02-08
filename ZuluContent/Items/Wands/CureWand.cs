using Server.Spells.Second;

namespace Server.Items
{
    public class CureWand : BaseWand
    {

        [Constructible]
        public CureWand() : base(WandEffect.Cure, 5, 30)
        {
        }

        [Constructible]
        public CureWand(Serial serial) : base(serial)
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
            Cast(new CureSpell(from, this));
        }
    }
}
