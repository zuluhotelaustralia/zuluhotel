// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class SpikeIngot : BaseIngot {
		[Constructable]
		public SpikeIngot() : this( 1 ) {}

		[Constructable]
		public SpikeIngot( int amount ) : base( CraftResource.Spike, amount ) {
			this.Hue = 0x4c7;
		}

		public SpikeIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "spike ingot"; } }
		public string ResourceName { get { return "spike"; } }
		public double ResourceQuality { get { return 1.05; } }

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
