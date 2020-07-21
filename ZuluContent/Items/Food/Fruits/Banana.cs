namespace Server.Items
{
    [Flipable( 0x171f, 0x1720 )]
    public class Banana : Food
    {

        [Constructible]
public Banana() : this( 1 )
        {
        }


        [Constructible]
public Banana( int amount ) : base( amount, 0x171f )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Banana( Serial serial ) : base( serial )
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
