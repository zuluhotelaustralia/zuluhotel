// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class OakBoard : Board {
		[Constructable]
		public OakBoard() : this( 1 ) {}

		[Constructable]
		public OakBoard( int amount ) : base( CraftResource.Oak, amount ) {
			this.Name = "oak board";
			this.Hue = 1045;
		}

		public string ResourceName { get { return "oak"; } }
		public double ResourceQuality { get { return 1.15; } }

		public OakBoard( Serial serial ) : base( serial ) {}

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
