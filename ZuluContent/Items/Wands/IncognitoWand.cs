using Server.Spells.Fifth;

namespace Server.Items
{
    public class IncognitoWand : BaseWand
    {
        [Constructible]
        public IncognitoWand() : base(WandEffect.Incognito, 2, 10)
        {
        }

        [Constructible]
        public IncognitoWand(Serial serial) : base(serial)
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
            Cast(new IncognitoSpell(from, this));
        }
    }
}
