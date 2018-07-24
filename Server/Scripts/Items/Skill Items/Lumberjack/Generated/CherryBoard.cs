// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class CherryBoard : Board {
		[Constructable]
		public CherryBoard() : this( 1 ) {}

		[Constructable]
		public CherryBoard( int amount ) : base( CraftResource.Cherry, amount ) {
			this.Hue = 5716;
		}

		public string ResourceName { get { return "cherry"; } }
		public double ResourceQuality { get { return 1.10; } }

		public CherryBoard( Serial serial ) : base( serial ) {}

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
