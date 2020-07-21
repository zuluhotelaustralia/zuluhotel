namespace Server.Items
{
    public class BrightlyColoredEggs : CookableFood
    {
        public override string DefaultName
        {
            get { return "brightly colored eggs"; }
        }


        [Constructible]
public BrightlyColoredEggs() : base( 0x9B5, 15 )
        {
            Weight = 0.5;
            Hue = 3 + Utility.Random( 20 ) * 5;
        }

        [Constructible]
public BrightlyColoredEggs( Serial serial ) : base( serial )
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
            return new FriedEggs();
        }
    }
}
