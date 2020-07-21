namespace Server.Items
{
    public class Jug : BaseBeverage
    {
        public override int BaseLabelNumber { get { return 1042965; } } // a jug of Ale
        public override int MaxQuantity { get { return 10; } }
        public override bool Fillable { get { return false; } }

        public override int ComputeItemID()
        {
            if( !IsEmpty )
                return 0x9C8;

            return 0;
        }


        [Constructible]
public Jug( BeverageType type )
            : base( type )
        {
            Weight = 1.0;
        }

        [Constructible]
public Jug( Serial serial )
            : base( serial )
        {
        }

        public override void Serialize( IGenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int)1 ); // version
        }

        public override void Deserialize( IGenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        }
    }
}
