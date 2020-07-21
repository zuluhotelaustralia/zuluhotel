namespace Server.Items
{
    public class Ribs : Food
    {

        [Constructible]
public Ribs() : this( 1 )
        {
        }


        [Constructible]
public Ribs( int amount ) : base( amount, 0x9F2 )
        {
            Weight = 1.0;
            FillFactor = 5;
        }

        [Constructible]
public Ribs( Serial serial ) : base( serial )
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
