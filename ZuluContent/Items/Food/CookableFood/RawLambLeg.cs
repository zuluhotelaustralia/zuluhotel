namespace Server.Items
{
    public class RawLambLeg : CookableFood
    {

        [Constructible]
public RawLambLeg() : this( 1 )
        {
        }


        [Constructible]
public RawLambLeg( int amount ) : base( 0x1609, 10 )
        {
            Stackable = true;
            Amount = amount;
        }

        [Constructible]
public RawLambLeg( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( IGenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 1 ); // version
        }

        public override void Deserialize( IGenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            if ( version == 0 && Weight == 1 )
                Weight = -1;
        }

        public override Food Cook()
        {
            return new LambLeg();
        }
    }
}
