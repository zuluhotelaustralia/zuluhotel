namespace Server.Items
{
    public class UnbakedApplePie : CookableFood
    {
        public override int LabelNumber{ get{ return 1041336; } } // unbaked apple pie


        [Constructible]
public UnbakedApplePie() : base( 0x1042, 25 )
        {
            Weight = 1.0;
        }

        [Constructible]
public UnbakedApplePie( Serial serial ) : base( serial )
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
            return new ApplePie();
        }
    }
}
