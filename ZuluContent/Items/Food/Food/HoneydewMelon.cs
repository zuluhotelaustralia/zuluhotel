namespace Server.Items
{
    [Flipable( 0xC74, 0xC75 )]
    public class HoneydewMelon : Food
    {

        [Constructible]
public HoneydewMelon() : this( 1 )
        {
        }


        [Constructible]
public HoneydewMelon( int amount ) : base( amount, 0xC74 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public HoneydewMelon( Serial serial ) : base( serial )
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
