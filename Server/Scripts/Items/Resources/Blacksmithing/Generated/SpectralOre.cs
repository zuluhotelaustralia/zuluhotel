// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class SpectralOre : BaseOre {
		[Constructable]
		public SpectralOre() : this( 1 ) {}

		[Constructable]
		public SpectralOre( int amount ) : base( CraftResource.Spectral, amount ) {
			this.Hue = 2744;
		}

		public SpectralOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Spectral Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new SpectralIngot();
		}
	}	
}
