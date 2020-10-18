namespace Server.Items
{
    public class DragonsBlood : BaseReagent
    {
        [Constructible]
        public DragonsBlood()
            : this(1)
        {
        }


        [Constructible]
        public DragonsBlood(int amount)
            : base(0xF82, amount)
        {
        }

        [Constructible]
        public DragonsBlood(Serial serial)
            : base(serial)
        {
        }

        public override double DefaultWeight
        {
            get { return 0.1; }
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