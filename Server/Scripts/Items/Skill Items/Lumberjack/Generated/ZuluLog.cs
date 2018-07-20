// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class ZuluLog : Log {
		[Constructable]
		public ZuluLog() : this( 1 ) {}

		[Constructable]
		public ZuluLog( int amount ) : base( CraftResource.Zulu, amount ) {
			this.Name = "zulu log";
			this.Hue = 1160;
		}

		public ZuluLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,  130 , new ZuluBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
