namespace Server.Items
{
    public class Cookies : Food
    {

        [Constructible]
public Cookies() : base( 0x160b )
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 4;
        }

        [Constructible]
public Cookies( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( IGenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 0 ); // version
        }

        public override void Deserialize( IGenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        }
    }
}
