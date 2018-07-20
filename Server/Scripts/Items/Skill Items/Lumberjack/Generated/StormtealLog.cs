// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class StormtealLog : Log {
		[Constructable]
		public StormtealLog() : this( 1 ) {}

		[Constructable]
		public StormtealLog( int amount ) : base( CraftResource.Stormteal, amount ) {
			this.Name = "stormteal log";
			this.Hue = 1346;
		}

		public StormtealLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,  114 , new StormtealBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
