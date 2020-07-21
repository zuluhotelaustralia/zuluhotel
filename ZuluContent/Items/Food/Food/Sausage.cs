namespace Server.Items
{
    public class Sausage : Food
    {

        [Constructible]
public Sausage() : this( 1 )
        {
        }


        [Constructible]
public Sausage( int amount ) : base( amount, 0x9C0 )
        {
            Weight = 1.0;
            FillFactor = 4;
        }

        [Constructible]
public Sausage( Serial serial ) : base( serial )
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
