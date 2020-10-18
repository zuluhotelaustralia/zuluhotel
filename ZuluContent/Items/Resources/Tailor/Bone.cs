namespace Server.Items
{
    public class Bone : BaseReagent
    {
        [Constructible]
        public Bone() : this(1)
        {
        }


        [Constructible]
        public Bone(int amount) : base(0xf7e)
        {
            Stackable = true;
            Amount = amount;
            Weight = 1.0;
        }

        [Constructible]
        public Bone(Serial serial) : base(serial)
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