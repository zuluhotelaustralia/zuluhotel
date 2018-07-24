// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class DullCopperIngot : BaseIngot {
		[Constructable]
		public DullCopperIngot() : this( 1 ) {}

		[Constructable]
		public DullCopperIngot( int amount ) : base( CraftResource.DullCopper, amount ) {
			this.Hue = 0x3ea;
		}

		public DullCopperIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "dull copper ingot"; } }
		public string ResourceName { get { return "dull copper"; } }
		public double ResourceQuality { get { return 1.15; } }

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
