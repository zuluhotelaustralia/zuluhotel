// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class BloodwoodLog : Log {
		[Constructable]
		public BloodwoodLog() : this( 1 ) {}

		[Constructable]
		public BloodwoodLog( int amount ) : base( CraftResource.Bloodwood, amount ) {}

		public BloodwoodLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,  122 , new BloodwoodBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
