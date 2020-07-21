namespace Server.Items
{
    public class PewterMug : BaseBeverage
    {
        public override int BaseLabelNumber { get { return 1042994; } } // a pewter mug with Ale
        public override int MaxQuantity { get { return 1; } }

        public override int ComputeItemID()
        {
            if( ItemID >= 0xFFF && ItemID <= 0x1002 )
                return ItemID;

            return 0xFFF;
        }


        [Constructible]
public PewterMug()
        {
            Weight = 1.0;
        }


        [Constructible]
public PewterMug( BeverageType type )
            : base( type )
        {
            Weight = 1.0;
        }

        [Constructible]
public PewterMug( Serial serial )
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
