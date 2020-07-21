namespace Server.Items
{
    public class FruitPie : Food
    {
        public override int LabelNumber{ get{ return 1041346; } } // baked fruit pie


        [Constructible]
public FruitPie() : base( 0x1041 )
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 5;
        }

        [Constructible]
public FruitPie( Serial serial ) : base( serial )
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
