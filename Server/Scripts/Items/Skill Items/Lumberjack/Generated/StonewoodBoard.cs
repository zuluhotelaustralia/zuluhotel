// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class StonewoodBoard : Board {
		[Constructable]
		public StonewoodBoard() : this( 1 ) {}

		[Constructable]
		public StonewoodBoard( int amount ) : base( CraftResource.Stonewood, amount ) {}

		public StonewoodBoard( Serial serial ) : base( serial ) {}

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
