// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class PurplePassionBoard : Board {
		[Constructable]
		public PurplePassionBoard() : this( 1 ) {}

		[Constructable]
		public PurplePassionBoard( int amount ) : base( CraftResource.PurplePassion, amount ) {
			this.Name = "purple passion board";
			this.Hue = 515;
		}

		public PurplePassionBoard( Serial serial ) : base( serial ) {}

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
