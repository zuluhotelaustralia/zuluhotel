// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class IceRockIngot : BaseIngot {
		[Constructable]
		public IceRockIngot() : this( 1 ) {}

		[Constructable]
		public IceRockIngot( int amount ) : base( CraftResource.IceRock, amount ) {
			this.Name = "ice rock ingot";
			this.Hue = 0x480;
		}

		public IceRockIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "ice rock ingot"; } }
		public string ResourceName { get { return "ice rock"; } }
		public double ResourceQuality { get { return 1.05; } }

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
