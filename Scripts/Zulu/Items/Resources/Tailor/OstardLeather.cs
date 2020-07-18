namespace Server.Items
{
    [FlipableAttribute(0x1081, 0x1082)]
    public class OstardLeather : BaseLeather
    {
        [Constructable]
        public OstardLeather() : this(1)
        {
        }

        [Constructable]
        public OstardLeather(int amount) : base(CraftResource.OstardLeather, amount)
        {
            this.Hue = 0x415;
        }

        public OstardLeather(Serial serial) : base(serial)
        {
        }

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
