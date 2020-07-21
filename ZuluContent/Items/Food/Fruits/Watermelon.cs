namespace Server.Items
{
    public class Watermelon : Food
    {

        [Constructible]
public Watermelon() : this( 1 )
        {
        }


        [Constructible]
public Watermelon( int amount ) : base( amount, 0xC5C )
        {
            Weight = 5.0;
            FillFactor = 5;
        }

        [Constructible]
public Watermelon( Serial serial ) : base( serial )
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
                if ( FillFactor == 2 )
                    FillFactor = 5;

                if ( Weight == 2.0 )
                    Weight = 5.0;
            }
        }
    }
}
