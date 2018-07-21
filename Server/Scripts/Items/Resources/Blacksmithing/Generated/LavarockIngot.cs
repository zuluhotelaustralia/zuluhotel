// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class LavarockIngot : BaseIngot {
		[Constructable]
		public LavarockIngot() : this( 1 ) {}

		[Constructable]
		public LavarockIngot( int amount ) : base( CraftResource.Lavarock, amount ) {
			this.Name = "lavarock ingot";
			this.Hue = 0x486;
		}

		public LavarockIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "lavarock ingot"; } }
		public string ResourceName { get { return "lavarock"; } }
		public double ResourceQuality { get { return 1.35; } }

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
