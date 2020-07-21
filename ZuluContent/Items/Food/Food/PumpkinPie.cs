namespace Server.Items
{
    public class PumpkinPie : Food
    {
        public override int LabelNumber{ get{ return 1041348; } } // baked pumpkin pie


        [Constructible]
public PumpkinPie() : base( 0x1041 )
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 5;
        }

        [Constructible]
public PumpkinPie( Serial serial ) : base( serial )
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
