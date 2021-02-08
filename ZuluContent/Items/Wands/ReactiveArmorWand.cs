using Server.Spells.First;

namespace Server.Items
{
    public class ReactiveArmorWand : BaseWand
    {

        [Constructible]
        public ReactiveArmorWand() : base(WandEffect.ReactiveArmor, 5, 30)
        {
        }

        [Constructible]
        public ReactiveArmorWand(Serial serial) : base(serial)
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
            Cast(new ReactiveArmorSpell(from, this));
        }
    }
}
