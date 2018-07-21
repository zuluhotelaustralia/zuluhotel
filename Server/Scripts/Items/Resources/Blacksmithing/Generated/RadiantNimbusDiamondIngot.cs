// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class RadiantNimbusDiamondIngot : BaseIngot {
		[Constructable]
		public RadiantNimbusDiamondIngot() : this( 1 ) {}

		[Constructable]
		public RadiantNimbusDiamondIngot( int amount ) : base( CraftResource.RadiantNimbusDiamond, amount ) {
			this.Name = "radiant nimbus diamond ingot";
			this.Hue = 0x498;
		}

		public RadiantNimbusDiamondIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "radiant nimbus diamond ingot"; } }
		public string ResourceName { get { return "radiant nimbus diamond"; } }
		public double ResourceQuality { get { return 2.25; } }

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
