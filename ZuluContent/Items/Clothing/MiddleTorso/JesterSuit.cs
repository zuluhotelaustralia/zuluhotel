namespace Server.Items
{
    [Flipable( 0x1f9f, 0x1fa0 )]
    public class JesterSuit : BaseMiddleTorso
    {

        [Constructible]
public JesterSuit() : this( 0 )
        {
        }


        [Constructible]
public JesterSuit( int hue ) : base( 0x1F9F, hue )
        {
            Weight = 4.0;
        }

        [Constructible]
public JesterSuit( Serial serial ) : base( serial )
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
