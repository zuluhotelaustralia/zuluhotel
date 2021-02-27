namespace Server.Items
{
    [FlipableAttribute(0x170f, 0x1710)]
    public class Shoes : BaseClothing
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 2;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;

        [Constructible]
        public Shoes() : base(0x170F)
        {
            Weight = 2.0;
        }

        [Constructible]
        public Shoes(Serial serial) : base(serial)
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