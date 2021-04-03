namespace Server.Items
{
    public class Blackmoor : BaseReagent
    {
        [Constructible]
        public Blackmoor()
            : this(1)
        {
        }


        [Constructible]
        public Blackmoor(int amount)
            : base(0xF79, amount)
        {
        }

        [Constructible]
        public Blackmoor(Serial serial)
            : base(serial)
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