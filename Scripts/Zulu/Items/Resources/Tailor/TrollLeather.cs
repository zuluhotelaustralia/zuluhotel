namespace Server.Items
{
    [FlipableAttribute(0x1081, 0x1082)]
    public class TrollLeather : BaseLeather
    {
        [Constructable]
        public TrollLeather() : this(1)
        {
        }

        [Constructable]
        public TrollLeather(int amount) : base(CraftResource.TrollLeather, amount)
        {
            this.Hue = 0x54a;
        }

        public TrollLeather(Serial serial) : base(serial)
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
