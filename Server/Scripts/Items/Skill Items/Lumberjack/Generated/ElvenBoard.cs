// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class ElvenBoard : Board {
		[Constructable]
		public ElvenBoard() : this( 1 ) {}

		[Constructable]
		public ElvenBoard( int amount ) : base( CraftResource.Elven, amount ) {
			this.Name = "elven board";
			this.Hue = 1165;
		}

		public string ResourceName { get { return "elven"; } }
		public double ResourceQuality { get { return 2.10; } }

		public ElvenBoard( Serial serial ) : base( serial ) {}

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
