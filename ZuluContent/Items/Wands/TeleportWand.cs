using Server.Spells.Third;

namespace Server.Items
{
    public class TeleportWand : BaseWand
    {
        [Constructible]
        public TeleportWand() : base(WandEffect.Teleport, 5, 30)
        {
        }

        [Constructible]
        public TeleportWand(Serial serial) : base(serial)
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
            Cast(new TeleportSpell(from, this));
        }
    }
}
