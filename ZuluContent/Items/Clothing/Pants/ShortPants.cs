namespace Server.Items
{
    [Flipable( 0x152e, 0x152f )]
    public class ShortPants : BasePants
    {

        [Constructible]
public ShortPants() : this( 0 )
        {
        }


        [Constructible]
public ShortPants( int hue ) : base( 0x152E, hue )
        {
            Weight = 2.0;
        }

        [Constructible]
public ShortPants( Serial serial ) : base( serial )
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
