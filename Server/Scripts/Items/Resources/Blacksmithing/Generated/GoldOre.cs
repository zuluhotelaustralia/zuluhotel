// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class GoldOre : BaseOre
    {
        [Constructable]
        public GoldOre() : this(1) { }

        [Constructable]
        public GoldOre(int amount) : base(CraftResource.Gold, amount)
        {
            this.Hue = 2793;
        }

        public GoldOre(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new GoldIngot();
        }
    }
}
