// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class CrystalwoodBoard : Board {
		[Constructable]
		public CrystalwoodBoard() : this( 1 ) {}

		[Constructable]
		public CrystalwoodBoard( int amount ) : base( CraftResource.Crystalwood, amount ) {
			this.Name = "crystal wood board";
			this.Hue = 1170;
		}

		public CrystalwoodBoard( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}	
}
