// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class DripstoneIngot : BaseIngot {
		[Constructable]
		public DripstoneIngot() : this( 1 ) {}

		[Constructable]
		public DripstoneIngot( int amount ) : base( CraftResource.Dripstone, amount ) {
			this.Hue = 2771;
		}

		public DripstoneIngot( Serial serial ) : base( serial ) {}

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
