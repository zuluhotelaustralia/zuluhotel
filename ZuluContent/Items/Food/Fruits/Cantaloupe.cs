namespace Server.Items
{
    [Flipable( 0xc79, 0xc7a )]
    public class Cantaloupe : Food
    {

        [Constructible]
public Cantaloupe() : this( 1 )
        {
        }


        [Constructible]
public Cantaloupe( int amount ) : base( amount, 0xc79 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Cantaloupe( Serial serial ) : base( serial )
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
