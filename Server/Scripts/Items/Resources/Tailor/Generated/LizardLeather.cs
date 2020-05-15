namespace Server.Items
{
    [FlipableAttribute(0x1081, 0x1082)]
    public class LizardLeather : BaseLeather
    {
        [Constructable]
        public LizardLeather() : this(1)
        {
        }

        [Constructable]
        public LizardLeather(int amount) : base(CraftResource.LizardLeather, amount)
        {
            this.Hue = 0x852;
        }

        public LizardLeather(Serial serial) : base(serial)
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
