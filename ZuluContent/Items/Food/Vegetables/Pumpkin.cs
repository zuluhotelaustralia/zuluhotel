namespace Server.Items
{
    [Flipable( 0xC6A, 0xC6B )]
    public class Pumpkin : Food
    {

        [Constructible]
public Pumpkin() : this( 1 )
        {
        }


        [Constructible]
public Pumpkin( int amount ) : base( amount, 0xC6A )
        {
            Weight = 1.0;
            FillFactor = 8;
        }

        [Constructible]
public Pumpkin( Serial serial ) : base( serial )
        {
        }
        public override void Serialize( IGenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 1 ); // version
        }

        public override void Deserialize( IGenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            if ( version < 1 )
            {
                if ( FillFactor == 4 )
                    FillFactor = 8;

                if ( Weight == 5.0 )
                    Weight = 1.0;
            }
        }
    }
}
