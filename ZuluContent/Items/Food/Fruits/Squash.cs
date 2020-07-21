namespace Server.Items
{
    [Flipable( 0xc72, 0xc73 )]
    public class Squash : Food
    {

        [Constructible]
public Squash() : this( 1 )
        {
        }


        [Constructible]
public Squash( int amount ) : base( amount, 0xc72 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Squash( Serial serial ) : base( serial )
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
