using Server.Mobiles;

namespace Server.Items
{
    public class BaseFishingTrophy : Item
    {
        public override string DefaultName => "fishing trophy";

        public BaseFishingTrophy(int itemID, Mobile winner) : base(itemID)
        {
            Weight = 1;
            Hue = 1160;
            Name = $"{winner.Name}'s {DefaultName}";
        }

        public BaseFishingTrophy( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( IGenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( 0 ); // version
        }

        public override void Deserialize( IGenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        }
    }
    
    public class FishingTrophy1 : BaseFishingTrophy
    {
        public FishingTrophy1(Mobile winner) : base(0x1E62, winner)
		{
        }

        public FishingTrophy1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( 0 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
    }
    
    public class FishingTrophy2 : BaseFishingTrophy
    {
        public FishingTrophy2(Mobile winner) : base(0x1E69, winner)
        {
        }

        public FishingTrophy2( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( IGenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( 0 ); // version
        }

        public override void Deserialize( IGenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        }
    }
}
