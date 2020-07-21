namespace Server.Items
{
    [Flipable( 0x1541, 0x1542 )]
    public class BodySash : BaseMiddleTorso
    {

        [Constructible]
public BodySash() : this( 0 )
        {
        }


        [Constructible]
public BodySash( int hue ) : base( 0x1541, hue )
        {
            Weight = 1.0;
        }

        [Constructible]
public BodySash( Serial serial ) : base( serial )
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
