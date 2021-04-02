namespace Server.Items
{
    public class DeadWood : BaseReagent
    {
        [Constructible]
        public DeadWood() : this(1)
        {
        }


        [Constructible]
        public DeadWood(int amount) : base(0xF90, amount)
        {
        }

        [Constructible]
        public DeadWood(Serial serial) : base(serial)
        {
        }


        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}