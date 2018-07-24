// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class VirginityIngot : BaseIngot {
		[Constructable]
		public VirginityIngot() : this( 1 ) {}

		[Constructable]
		public VirginityIngot( int amount ) : base( CraftResource.Virginity, amount ) {
			this.Hue = 0x482;
		}

		public VirginityIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "virginity ingot"; } }
		public string ResourceName { get { return "virginity"; } }
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
