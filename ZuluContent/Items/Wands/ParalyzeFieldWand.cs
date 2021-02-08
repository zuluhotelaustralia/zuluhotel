using Server.Spells.Sixth;

namespace Server.Items
{
    public class ParalyzeFieldWand : BaseWand
    {
        [Constructible]
        public ParalyzeFieldWand() : base(WandEffect.ParalyzeField, 2, 10)
        {
        }

        [Constructible]
        public ParalyzeFieldWand(Serial serial) : base(serial)
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
            Cast(new ParalyzeFieldSpell(from, this));
        }
    }
}
