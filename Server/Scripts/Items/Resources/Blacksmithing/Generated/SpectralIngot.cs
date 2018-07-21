// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class SpectralIngot : BaseIngot {
		[Constructable]
		public SpectralIngot() : this( 1 ) {}

		[Constructable]
		public SpectralIngot( int amount ) : base( CraftResource.Spectral, amount ) {
			this.Name = "spectral ingot";
			this.Hue = 0x483;
		}

		public SpectralIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "spectral ingot"; } }
		public string ResourceName { get { return "spectral"; } }
		public double ResourceQuality { get { return 1.10; } }

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
