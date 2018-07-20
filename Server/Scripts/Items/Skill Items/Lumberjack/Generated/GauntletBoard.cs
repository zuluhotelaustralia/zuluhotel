// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class GauntletBoard : Board {
		[Constructable]
		public GauntletBoard() : this( 1 ) {}

		[Constructable]
		public GauntletBoard( int amount ) : base( CraftResource.Gauntlet, amount ) {
			this.Name = "gauntlet board";
			this.Hue = 1284;
		}

		public GauntletBoard( Serial serial ) : base( serial ) {}

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
