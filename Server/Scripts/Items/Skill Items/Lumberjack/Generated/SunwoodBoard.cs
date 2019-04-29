// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class SunwoodBoard : Board {
		[Constructable]
		public SunwoodBoard() : this( 1 ) {}

		[Constructable]
		public SunwoodBoard( int amount ) : base( CraftResource.Sunwood, amount ) {
			this.Hue = 2766;
		}

		public string ResourceName { get { return "sunwood"; } }
		public double ResourceQuality { get { return 1.45; } }

		public SunwoodBoard( Serial serial ) : base( serial ) {}

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
