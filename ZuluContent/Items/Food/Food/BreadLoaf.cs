namespace Server.Items
{
    public class BreadLoaf : Food
    {

        [Constructible]
public BreadLoaf() : this( 1 )
        {
        }


        [Constructible]
public BreadLoaf( int amount ) : base( amount, 0x103B )
        {
            Weight = 1.0;
            FillFactor = 3;
        }

        [Constructible]
public BreadLoaf( Serial serial ) : base( serial )
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
