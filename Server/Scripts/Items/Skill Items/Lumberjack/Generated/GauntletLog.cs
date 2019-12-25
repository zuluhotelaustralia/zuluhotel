// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class GauntletLog : BaseLog {
		[Constructable]
		public GauntletLog() : this( 1 ) {}

		[Constructable]
		public GauntletLog( int amount ) : base( CraftResource.Gauntlet, amount ) {
			this.Hue = 2777;
		}

		public GauntletLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,   95 , new GauntletBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
