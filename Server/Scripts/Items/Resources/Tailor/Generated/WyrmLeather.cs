namespace Server.Items
{
    [FlipableAttribute(0x1081, 0x1082)]
    public class WyrmLeather : BaseLeather
    {
        [Constructable]
        public WyrmLeather() : this(1)
        {
        }

        [Constructable]
        public WyrmLeather(int amount) : base(CraftResource.WyrmLeather, amount)
        {
            this.Hue = 2747;
        }

        public WyrmLeather(Serial serial) : base(serial)
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
