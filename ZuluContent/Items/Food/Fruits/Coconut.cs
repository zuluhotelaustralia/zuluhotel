namespace Server.Items
{
    public class Coconut : Food
    {

        [Constructible]
public Coconut() : this( 1 )
        {
        }


        [Constructible]
public Coconut( int amount ) : base( amount, 0x1726 )
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        [Constructible]
public Coconut( Serial serial ) : base( serial )
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
