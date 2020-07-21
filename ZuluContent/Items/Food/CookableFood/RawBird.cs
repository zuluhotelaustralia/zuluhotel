namespace Server.Items
{
    public class RawBird : CookableFood
    {

        [Constructible]
public RawBird() : this( 1 )
        {
        }


        [Constructible]
public RawBird( int amount ) : base( 0x9B9, 10 )
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        [Constructible]
public RawBird( Serial serial ) : base( serial )
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

        public override Food Cook()
        {
            return new CookedBird();
        }
    }
}
