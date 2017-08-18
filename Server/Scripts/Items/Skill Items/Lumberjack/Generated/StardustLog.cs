// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class StardustLog : Log {
		[Constructable]
		public StardustLog() : this( 1 ) {}

		[Constructable]
		public StardustLog( int amount ) : base( CraftResource.Stardust, amount ) {}

		public StardustLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,  105 , new StardustBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
