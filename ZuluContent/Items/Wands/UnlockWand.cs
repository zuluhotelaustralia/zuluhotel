using Server.Spells.Third;

namespace Server.Items
{
    public class UnlockWand : BaseWand
    {
        [Constructible]
        public UnlockWand() : base(WandEffect.Unlock, 5, 30)
        {
        }

        [Constructible]
        public UnlockWand(Serial serial) : base(serial)
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
            Cast(new UnlockSpell(from, this));
        }
    }
}
