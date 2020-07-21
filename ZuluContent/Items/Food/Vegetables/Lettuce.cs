namespace Server.Items
{
    [Flipable( 0xc70, 0xc71 )]
    public class Lettuce : Food
    {

        [Constructible]
public Lettuce() : this( 1 )
        {
        }


        [Constructible]
public Lettuce( int amount ) : base( amount, 0xc70 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Lettuce( Serial serial ) : base( serial )
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
