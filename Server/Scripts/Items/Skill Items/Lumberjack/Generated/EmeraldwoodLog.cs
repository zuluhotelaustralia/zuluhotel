// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class EmeraldwoodLog : Log {
		[Constructable]
		public EmeraldwoodLog() : this( 1 ) {}

		[Constructable]
		public EmeraldwoodLog( int amount ) : base( CraftResource.Emeraldwood, amount ) {}

		public EmeraldwoodLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,  118 , new EmeraldwoodBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
