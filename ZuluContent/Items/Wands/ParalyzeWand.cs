using Server.Spells.Fifth;

namespace Server.Items
{
    public class ParalyzeWand : BaseWand
    {
        [Constructible]
        public ParalyzeWand() : base(WandEffect.Paralyze, 2, 10)
        {
        }

        [Constructible]
        public ParalyzeWand(Serial serial) : base(serial)
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
            Cast(new ParalyzeSpell(from, this));
        }
    }
}
