// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class PurplePassionLog : Log {
		[Constructable]
		public PurplePassionLog() : this( 1 ) {}

		[Constructable]
		public PurplePassionLog( int amount ) : base( CraftResource.PurplePassion, amount ) {
			this.Hue = 515;
		}

		public PurplePassionLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,   50 , new PurplePassionBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
