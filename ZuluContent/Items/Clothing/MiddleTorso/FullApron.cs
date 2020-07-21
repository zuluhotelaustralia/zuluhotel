namespace Server.Items
{
    [Flipable( 0x153d, 0x153e )]
    public class FullApron : BaseMiddleTorso
    {

        [Constructible]
public FullApron() : this( 0 )
        {
        }


        [Constructible]
public FullApron( int hue ) : base( 0x153d, hue )
        {
            Weight = 4.0;
        }

        [Constructible]
public FullApron( Serial serial ) : base( serial )
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
