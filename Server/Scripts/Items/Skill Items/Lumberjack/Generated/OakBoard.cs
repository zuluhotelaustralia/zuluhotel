// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class OakBoard : BaseBoard {
		[Constructable]
		public OakBoard() : this( 1 ) {}

		[Constructable]
		public OakBoard( int amount ) : base( CraftResource.Oak, amount ) {
			this.Hue = 1045;
		}

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
