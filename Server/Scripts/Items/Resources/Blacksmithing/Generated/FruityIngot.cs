// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class FruityIngot : BaseIngot
    {
        [Constructable]
        public FruityIngot() : this(1) { }

        [Constructable]
        public FruityIngot(int amount) : base(CraftResource.Fruity, amount)
        {
            this.Hue = 0x46e;
        }

        public FruityIngot(Serial serial) : base(serial) { }

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
    }
}
