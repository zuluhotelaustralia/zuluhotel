// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class FruityOre : BaseOre
    {
        [Constructable]
        public FruityOre() : this(1) { }

        [Constructable]
        public FruityOre(int amount) : base(CraftResource.Fruity, amount)
        {
            this.Hue = 0x46e;
        }

        public FruityOre(Serial serial) : base(serial) { }

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
            return new FruityIngot();
        }
    }
}
