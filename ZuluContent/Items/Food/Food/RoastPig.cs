namespace Server.Items
{
    public class RoastPig : Food
    {

        [Constructible]
public RoastPig() : this( 1 )
        {
        }


        [Constructible]
public RoastPig( int amount ) : base( amount, 0x9BB )
        {
            Weight = 45.0;
            FillFactor = 20;
        }

        [Constructible]
public RoastPig( Serial serial ) : base( serial )
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
