namespace Server.Items
{
    [Flipable( 0xc7b, 0xc7c )]
    public class Cabbage : Food
    {

        [Constructible]
public Cabbage() : this( 1 )
        {
        }


        [Constructible]
public Cabbage( int amount ) : base( amount, 0xc7b )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Cabbage( Serial serial ) : base( serial )
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
