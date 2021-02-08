using Server.Spells.First;

namespace Server.Items
{
    public class CreateFoodWand : BaseWand
    {
        [Constructible]
        public CreateFoodWand(Serial serial) : base(serial)
        {
        }

        [Constructible]
        public CreateFoodWand() : base(WandEffect.CreateFood, 5, 30)
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
            Cast(new CreateFoodSpell(from, this));
        }
    }
}
