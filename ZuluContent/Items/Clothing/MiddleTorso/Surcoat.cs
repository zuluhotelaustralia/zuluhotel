namespace Server.Items
{
    [Flipable( 0x1ffd, 0x1ffe )]
    public class Surcoat : BaseMiddleTorso
    {

        [Constructible]
public Surcoat() : this( 0 )
        {
        }


        [Constructible]
public Surcoat( int hue ) : base( 0x1FFD, hue )
        {
            Weight = 6.0;
        }

        [Constructible]
public Surcoat( Serial serial ) : base( serial )
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

            if ( Weight == 3.0 )
                Weight = 6.0;
        }
    }
}
