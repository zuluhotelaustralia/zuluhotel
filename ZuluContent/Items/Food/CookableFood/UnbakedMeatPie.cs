namespace Server.Items
{
    public class UnbakedMeatPie : CookableFood
    {
        public override int LabelNumber{ get{ return 1041338; } } // unbaked meat pie


        [Constructible]
public UnbakedMeatPie() : base( 0x1042, 25 )
        {
            Weight = 1.0;
        }

        [Constructible]
public UnbakedMeatPie( Serial serial ) : base( serial )
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
            return new MeatPie();
        }
    }
}
