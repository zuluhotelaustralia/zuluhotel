// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class PinetreeLog : Log {
		[Constructable]
		public PinetreeLog() : this( 1 ) {}

		[Constructable]
		public PinetreeLog( int amount ) : base( CraftResource.Pinetree, amount ) {
			this.Hue = 1132;
		}

		public PinetreeLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,   15 , new PinetreeBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
