// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class SilverleafBoard : Board {
		[Constructable]
		public SilverleafBoard() : this( 1 ) {}

		[Constructable]
		public SilverleafBoard( int amount ) : base( CraftResource.Silverleaf, amount ) {
			this.Hue = 2301;
		}

		public string ResourceName { get { return "silver leaf"; } }
		public double ResourceQuality { get { return 1.60; } }

		public SilverleafBoard( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}	
}
