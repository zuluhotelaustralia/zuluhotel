namespace Server.Items
{
    [Flipable(0x170d, 0x170e)]
    public class Sandals : BaseShoes
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 2;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;

        [Constructible]
        public Sandals() : base(0x170D)
        {
            Weight = 1.0;
        }

        [Constructible]
        public Sandals(Serial serial) : base(serial)
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