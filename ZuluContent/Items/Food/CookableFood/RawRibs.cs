namespace Server.Items
{
    public class RawRibs : CookableFood
    {

        [Constructible]
public RawRibs() : this( 1 )
        {
        }


        [Constructible]
public RawRibs( int amount ) : base( 0x9F1, 10 )
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        [Constructible]
public RawRibs( Serial serial ) : base( serial )
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
            return new Ribs();
        }
    }
}
