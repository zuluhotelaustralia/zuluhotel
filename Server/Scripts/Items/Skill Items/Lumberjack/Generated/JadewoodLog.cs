// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class JadewoodLog : BaseLog {
		[Constructable]
		public JadewoodLog() : this( 1 ) {}

		[Constructable]
		public JadewoodLog( int amount ) : base( CraftResource.Jadewood, amount ) {
			this.Hue = 1162;
		}

		public JadewoodLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,   68 , new JadewoodBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
