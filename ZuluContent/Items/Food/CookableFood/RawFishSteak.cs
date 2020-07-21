namespace Server.Items
{
    public class RawFishSteak : CookableFood
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }


        [Constructible]
public RawFishSteak() : this( 1 )
        {
        }


        [Constructible]
public RawFishSteak( int amount ) : base( 0x097A, 10 )
        {
            Stackable = true;
            Amount = amount;
        }

        [Constructible]
public RawFishSteak( Serial serial ) : base( serial )
        {
        }

        public override Food Cook()
        {
            return new FishSteak();
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
