using Server.Spells.Second;

namespace Server.Items
{
    public class ProtectionWand : BaseWand
    {

        [Constructible]
        public ProtectionWand() : base(WandEffect.Protection, 5, 30)
        {
        }

        [Constructible]
        public ProtectionWand(Serial serial) : base(serial)
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
            Cast(new ProtectionSpell(from, this));
        }
    }
}
