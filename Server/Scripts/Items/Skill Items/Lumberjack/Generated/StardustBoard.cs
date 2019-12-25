// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class StardustBoard : Board {
		[Constructable]
		public StardustBoard() : this( 1 ) {}

		[Constructable]
		public StardustBoard( int amount ) : base( CraftResource.Stardust, amount ) {
			this.Hue = 2751;
		}

		public StardustBoard( Serial serial ) : base( serial ) {}

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
