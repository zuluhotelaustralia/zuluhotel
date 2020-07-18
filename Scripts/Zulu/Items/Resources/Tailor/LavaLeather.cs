namespace Server.Items
{
    [FlipableAttribute(0x1081, 0x1082)]
    public class LavaLeather : BaseLeather
    {
        [Constructable]
        public LavaLeather() : this(1)
        {
        }

        [Constructable]
        public LavaLeather(int amount) : base(CraftResource.LavaLeather, amount)
        {
            this.Hue = 2747;
        }

        public LavaLeather(Serial serial) : base(serial)
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
