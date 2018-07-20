// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class StonewoodLog : Log {
		[Constructable]
		public StonewoodLog() : this( 1 ) {}

		[Constructable]
		public StonewoodLog( int amount ) : base( CraftResource.Stonewood, amount ) {
			this.Name = "stonewood log";
			this.Hue = 1154;
		}

		public StonewoodLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,   84 , new StonewoodBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
