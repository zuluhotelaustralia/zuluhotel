namespace Server.Items
{
    [Flipable( 0xc6d, 0xc6e )]
    public class Onion : Food
    {

        [Constructible]
public Onion() : this( 1 )
        {
        }


        [Constructible]
public Onion( int amount ) : base( amount, 0xc6d )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Onion( Serial serial ) : base( serial )
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
