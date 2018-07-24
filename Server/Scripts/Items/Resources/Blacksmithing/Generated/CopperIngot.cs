// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class CopperIngot : BaseIngot {
		[Constructable]
		public CopperIngot() : this( 1 ) {}

		[Constructable]
		public CopperIngot( int amount ) : base( CraftResource.Copper, amount ) {
			this.Hue = 0x602;
		}

		public CopperIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "copper ingot"; } }
		public string ResourceName { get { return "copper"; } }
		public double ResourceQuality { get { return 1.20; } }

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
