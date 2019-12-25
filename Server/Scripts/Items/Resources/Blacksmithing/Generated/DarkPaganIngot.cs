// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class DarkPaganIngot : BaseIngot {
		[Constructable]
		public DarkPaganIngot() : this( 1 ) {}

		[Constructable]
		public DarkPaganIngot( int amount ) : base( CraftResource.DarkPagan, amount ) {
			this.Hue = 0x46b;
		}

		public DarkPaganIngot( Serial serial ) : base( serial ) {}

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
