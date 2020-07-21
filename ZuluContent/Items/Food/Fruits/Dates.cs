namespace Server.Items
{
    public class Dates : Food
    {

        [Constructible]
public Dates() : this( 1 )
        {
        }


        [Constructible]
public Dates( int amount ) : base( amount, 0x1727 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Dates( Serial serial ) : base( serial )
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
