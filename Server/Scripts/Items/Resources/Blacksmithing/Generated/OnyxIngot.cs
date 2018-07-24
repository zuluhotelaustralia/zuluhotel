// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class OnyxIngot : BaseIngot {
		[Constructable]
		public OnyxIngot() : this( 1 ) {}

		[Constructable]
		public OnyxIngot( int amount ) : base( CraftResource.Onyx, amount ) {
			this.Hue = 0x455;
		}

		public OnyxIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "onyx ingot"; } }
		public string ResourceName { get { return "onyx"; } }
		public double ResourceQuality { get { return 1.25; } }

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
