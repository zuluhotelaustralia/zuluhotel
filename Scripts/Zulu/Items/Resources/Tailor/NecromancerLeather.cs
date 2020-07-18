namespace Server.Items
{
    [FlipableAttribute(0x1081, 0x1082)]
    public class NecromancerLeather : BaseLeather
    {
        [Constructable]
        public NecromancerLeather() : this(1)
        {
        }

        [Constructable]
        public NecromancerLeather(int amount) : base(CraftResource.NecromancerLeather, amount)
        {
            this.Hue = 84;
        }

        public NecromancerLeather(Serial serial) : base(serial)
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
