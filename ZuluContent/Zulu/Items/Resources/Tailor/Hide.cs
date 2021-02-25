namespace Server.Items
{
    [FlipableAttribute(0x1079, 0x1078)]
    public class Hide : BaseHide
    {
        [Constructible]
        public Hide() : this(1)
        {
        }


        [Constructible]
        public Hide(int amount) : base(CraftResource.RegularLeather, amount)
        {
        }

        [Constructible]
        public Hide(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}