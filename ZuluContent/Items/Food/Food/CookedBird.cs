namespace Server.Items
{
    public class CookedBird : Food
    {

        [Constructible]
public CookedBird() : this( 1 )
        {
        }


        [Constructible]
public CookedBird( int amount ) : base( amount, 0x9B7 )
        {
            Weight = 1.0;
            FillFactor = 5;
        }

        [Constructible]
public CookedBird( Serial serial ) : base( serial )
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
