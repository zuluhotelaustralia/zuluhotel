namespace Server.Items
{
    public class WyrmsHeart : BaseReagent
    {
        [Constructible]
        public WyrmsHeart()
            : this(1)
        {
        }


        [Constructible]
        public WyrmsHeart(int amount)
            : base(0xF91, amount)
        {
        }

        [Constructible]
        public WyrmsHeart(Serial serial)
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