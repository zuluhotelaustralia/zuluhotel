// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class CherryLog : BaseLog {
		[Constructable]
		public CherryLog() : this( 1 ) {}

		[Constructable]
		public CherryLog( int amount ) : base( CraftResource.Cherry, amount ) {
			this.Hue = 716;
		}

		public CherryLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,   28 , new CherryBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
