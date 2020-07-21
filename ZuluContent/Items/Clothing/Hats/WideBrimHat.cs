namespace Server.Items
{
    public class WideBrimHat : BaseHat
    {
        public override int InitMinHits{ get{ return 20; } }
        public override int InitMaxHits{ get{ return 30; } }


        [Constructible]
public WideBrimHat() : this( 0 )
        {
        }


        [Constructible]
public WideBrimHat( int hue ) : base( 0x1714, hue )
        {
            Weight = 1.0;
        }

        [Constructible]
public WideBrimHat( Serial serial ) : base( serial )
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
