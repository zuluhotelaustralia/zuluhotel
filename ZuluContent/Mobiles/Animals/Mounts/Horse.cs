namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.BrownHorse", "Server.Mobiles.DirtyHorse", "Server.Mobiles.GrayHorse",
        "Server.Mobiles.TanHorse")]
    public class Horse : BaseMount
    {
        [Constructible]
        public Horse() : base("Horse")
        {
        }

        [Constructible]
        public Horse(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}