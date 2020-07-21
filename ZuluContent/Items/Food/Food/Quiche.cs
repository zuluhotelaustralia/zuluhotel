namespace Server.Items
{
    public class Quiche : Food
    {
        public override int LabelNumber{ get{ return 1041345; } } // baked quiche


        [Constructible]
public Quiche() : base( 0x1041 )
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 5;
        }

        [Constructible]
public Quiche( Serial serial ) : base( serial )
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
