using Server.Spells.Third;

namespace Server.Items
{
    public class MagicLockWand : BaseWand
    {
        [Constructible]
        public MagicLockWand() : base(WandEffect.MagicLock, 5, 30)
        {
        }

        [Constructible]
        public MagicLockWand(Serial serial) : base(serial)
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
            Cast(new MagicLockSpell(from, this));
        }
    }
}
