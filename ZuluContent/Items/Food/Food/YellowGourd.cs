namespace Server.Items
{
    [Flipable( 0xC64, 0xC65 )]
    public class YellowGourd : Food
    {

        [Constructible]
public YellowGourd() : this( 1 )
        {
        }


        [Constructible]
public YellowGourd( int amount ) : base( amount, 0xC64 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public YellowGourd( Serial serial ) : base( serial )
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
