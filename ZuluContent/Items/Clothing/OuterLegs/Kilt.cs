namespace Server.Items
{
    [Flipable( 0x1537, 0x1538 )]
    public class Kilt : BaseOuterLegs
    {

        [Constructible]
public Kilt() : this( 0 )
        {
        }


        [Constructible]
public Kilt( int hue ) : base( 0x1537, hue )
        {
            Weight = 2.0;
        }

        [Constructible]
public Kilt( Serial serial ) : base( serial )
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
