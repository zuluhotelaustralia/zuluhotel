// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class CherryBoard : BaseBoard {
		[Constructable]
		public CherryBoard() : this( 1 ) {}

		[Constructable]
		public CherryBoard( int amount ) : base( CraftResource.Cherry, amount ) {
			this.Hue = 716;
		}

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
