namespace Server.Mobiles
{
    public class Minter : Banker
    {
        public override NpcGuild NpcGuild => NpcGuild.MerchantsGuild;


        [Constructible]
        public Minter()
        {
            Title = "the Minter";
        }

        [Constructible]
        public Minter(Serial serial) : base(serial)
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