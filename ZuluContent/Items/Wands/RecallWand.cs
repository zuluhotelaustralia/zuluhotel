using Server.Spells.Fourth;

namespace Server.Items
{
    public class RecallWand : BaseWand
    {
        [Constructible]
        public RecallWand() : base(WandEffect.Recall, 5, 30)
        {
        }

        [Constructible]
        public RecallWand(Serial serial) : base(serial)
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
            Cast(new RecallSpell(from, this));
        }
    }
}
