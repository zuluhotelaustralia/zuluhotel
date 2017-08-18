// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class ElvenBoard : Board {
		[Constructable]
		public ElvenBoard() : this( 1 ) {}

		[Constructable]
		public ElvenBoard( int amount ) : base( CraftResource.Elven, amount ) {}

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
