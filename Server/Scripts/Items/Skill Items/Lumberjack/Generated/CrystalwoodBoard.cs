// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class CrystalwoodBoard : Board {
		[Constructable]
		public CrystalwoodBoard() : this( 1 ) {}

		[Constructable]
		public CrystalwoodBoard( int amount ) : base( CraftResource.Crystalwood, amount ) {
			this.Hue = 1170;
		}

		public string ResourceName { get { return "crystal wood"; } }
		public double ResourceQuality { get { return 1.80; } }

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
