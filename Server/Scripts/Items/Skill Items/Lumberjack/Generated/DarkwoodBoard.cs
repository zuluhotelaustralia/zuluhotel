// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class DarkwoodBoard : Board {
		[Constructable]
		public DarkwoodBoard() : this( 1 ) {}

		[Constructable]
		public DarkwoodBoard( int amount ) : base( CraftResource.Darkwood, amount ) {
			this.Name = "darkwood board";
			this.Hue = 1109;
		}

		public DarkwoodBoard( Serial serial ) : base( serial ) {}

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
