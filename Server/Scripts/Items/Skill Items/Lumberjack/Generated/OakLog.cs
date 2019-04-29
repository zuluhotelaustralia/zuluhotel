// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class OakLog : Log {
		[Constructable]
		public OakLog() : this( 1 ) {}

		[Constructable]
		public OakLog( int amount ) : base( CraftResource.Oak, amount ) {
			this.Hue = 1045;
		}

		public OakLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,   39 , new OakBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
