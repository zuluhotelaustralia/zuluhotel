namespace Server.Items
{
    public class FrenchBread : Food
    {

        [Constructible]
public FrenchBread() : this( 1 )
        {
        }


        [Constructible]
public FrenchBread( int amount ) : base( amount, 0x98C )
        {
            Weight = 2.0;
            FillFactor = 3;
        }

        [Constructible]
public FrenchBread( Serial serial ) : base( serial )
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
