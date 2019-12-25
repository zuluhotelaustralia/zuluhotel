// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class BronzeIngot : BaseIngot {
		[Constructable]
		public BronzeIngot() : this( 1 ) {}

		[Constructable]
		public BronzeIngot( int amount ) : base( CraftResource.Bronze, amount ) {
			this.Hue = 0x45e;
		}

		public BronzeIngot( Serial serial ) : base( serial ) {}

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
