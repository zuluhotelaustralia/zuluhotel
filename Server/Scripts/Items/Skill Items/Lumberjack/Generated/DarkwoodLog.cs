// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class DarkwoodLog : Log {
		[Constructable]
		public DarkwoodLog() : this( 1 ) {}

		[Constructable]
		public DarkwoodLog( int amount ) : base( CraftResource.Darkwood, amount ) {
			this.Hue = 1109;
		}

		public DarkwoodLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,   77 , new DarkwoodBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
