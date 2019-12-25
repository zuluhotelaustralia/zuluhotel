// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class IceRockIngot : BaseIngot {
		[Constructable]
		public IceRockIngot() : this( 1 ) {}

		[Constructable]
		public IceRockIngot( int amount ) : base( CraftResource.IceRock, amount ) {
			this.Hue = 0x480;
		}

		public IceRockIngot( Serial serial ) : base( serial ) {}

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
