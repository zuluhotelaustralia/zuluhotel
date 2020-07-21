namespace Server.Items
{
    [Flipable]
    public class Robe : BaseOuterTorso
    {

        [Constructible]
public Robe() : this( 0 )
        {
        }


        [Constructible]
public Robe( int hue ) : base( 0x1F03, hue )
        {
            Weight = 3.0;
        }

        [Constructible]
public Robe( Serial serial ) : base( serial )
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
