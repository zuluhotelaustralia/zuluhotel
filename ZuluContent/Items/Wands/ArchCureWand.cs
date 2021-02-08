using Server.Spells.Fourth;

namespace Server.Items
{
    public class ArchCureWand : BaseWand
    {
        [Constructible]
        public ArchCureWand() : base(WandEffect.ArchCure, 5, 30)
        {
        }

        [Constructible]
        public ArchCureWand(Serial serial) : base(serial)
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
            Cast(new ArchCureSpell(from, this));
        }
    }
}
