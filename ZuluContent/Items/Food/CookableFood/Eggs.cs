namespace Server.Items
{
    public class Eggs : CookableFood
    {

        [Constructible]
public Eggs() : this( 1 )
        {
        }


        [Constructible]
public Eggs( int amount ) : base( 0x9B5, 15 )
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        [Constructible]
public Eggs( Serial serial ) : base( serial )
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

            if ( version < 1 )
            {
                Stackable = true;

                if ( Weight == 0.5 )
                    Weight = 1.0;
            }
        }

        public override Food Cook()
        {
            return new FriedEggs();
        }
    }
}
