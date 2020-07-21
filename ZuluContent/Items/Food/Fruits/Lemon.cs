namespace Server.Items
{
    public class Lemon : Food
    {

        [Constructible]
public Lemon() : this( 1 )
        {
        }


        [Constructible]
public Lemon( int amount ) : base( amount, 0x1728 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Lemon( Serial serial ) : base( serial )
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
