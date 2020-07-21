namespace Server.Items
{
    public class CakeMix : CookableFood
    {
        public override int LabelNumber{ get{ return 1041002; } } // cake mix


        [Constructible]
public CakeMix() : base( 0x103F, 40 )
        {
            Weight = 1.0;
        }

        [Constructible]
public CakeMix( Serial serial ) : base( serial )
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
            return new Cake();
        }
    }
}
