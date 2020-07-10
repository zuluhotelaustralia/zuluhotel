namespace Server.Items
{
    public class Cookies : Food
    {
        [Constructable]
        public Cookies() : base( 0x160b )
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 4;
        }

        public Cookies( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 0 ); // version
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        }
    }
}