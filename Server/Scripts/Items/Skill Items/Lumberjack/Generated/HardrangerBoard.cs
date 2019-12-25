// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class HardrangerBoard : BaseBoard {
		[Constructable]
		public HardrangerBoard() : this( 1 ) {}

		[Constructable]
		public HardrangerBoard( int amount ) : base( CraftResource.Hardranger, amount ) {
			this.Hue = 2778;
		}

		public HardrangerBoard( Serial serial ) : base( serial ) {}

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
