// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class StormtealBoard : Board {
		[Constructable]
		public StormtealBoard() : this( 1 ) {}

		[Constructable]
		public StormtealBoard( int amount ) : base( CraftResource.Stormteal, amount ) {
			this.Hue = 1346;
		}

		public StormtealBoard( Serial serial ) : base( serial ) {}

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
