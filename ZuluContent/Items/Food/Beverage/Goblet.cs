namespace Server.Items
{
    public class Goblet : BaseBeverage
    {
        public override int BaseLabelNumber { get { return 1043000; } } // a goblet of Ale
        public override int MaxQuantity { get { return 1; } }

        public override int ComputeItemID()
        {
            if( ItemID == 0x99A || ItemID == 0x9B3 || ItemID == 0x9BF || ItemID == 0x9CB )
                return ItemID;

            return 0x99A;
        }


        [Constructible]
public Goblet()
        {
            Weight = 1.0;
        }


        [Constructible]
public Goblet( BeverageType type )
            : base( type )
        {
            Weight = 1.0;
        }

        [Constructible]
public Goblet( Serial serial )
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
