namespace Server.Items
{
    public class FishSteak : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }


        [Constructible]
public FishSteak() : this( 1 )
        {
        }


        [Constructible]
public FishSteak( int amount ) : base( amount, 0x97B )
        {
            FillFactor = 3;
        }

        [Constructible]
public FishSteak( Serial serial ) : base( serial )
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
