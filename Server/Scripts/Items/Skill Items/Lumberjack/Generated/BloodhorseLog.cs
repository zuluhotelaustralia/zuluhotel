// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class BloodhorseLog : Log {
		[Constructable]
		public BloodhorseLog() : this( 1 ) {}

		[Constructable]
		public BloodhorseLog( int amount ) : base( CraftResource.Bloodhorse, amount ) {
			this.Hue = 2780;
		}

		public BloodhorseLog( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool Axe( Mobile from, BaseAxe axe ) {
			if ( !TryCreateBoards( from,  127 , new BloodhorseBoard() ) ) {
				return false;
			}
			return true;
		}
	}	
}
