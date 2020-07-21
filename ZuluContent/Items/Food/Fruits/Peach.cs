namespace Server.Items
{
    public class Peach : Food
    {

        [Constructible]
public Peach() : this( 1 )
        {
        }


        [Constructible]
public Peach( int amount ) : base( amount, 0x9D2 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Peach( Serial serial ) : base( serial )
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
