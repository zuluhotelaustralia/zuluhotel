using Server.Spells.Fifth;

namespace Server.Items
{
    public class MindBlastWand : BaseWand
    {
        [Constructible]
        public MindBlastWand() : base(WandEffect.MindBlast, 2, 10)
        {
        }

        [Constructible]
        public MindBlastWand(Serial serial) : base(serial)
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
            Cast(new MindBlastSpell(from, this));
        }
    }
}
