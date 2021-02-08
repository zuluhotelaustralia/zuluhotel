using Server.Spells.Second;

namespace Server.Items
{
    public class StrengthWand : BaseWand
    {        
        [Constructible]
        public StrengthWand() : base(WandEffect.Strength, 5, 30)
        {
        }

        [Constructible]
        public StrengthWand(Serial serial) : base(serial)
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
            Cast(new StrengthSpell(from, this));
        }
    }
}
