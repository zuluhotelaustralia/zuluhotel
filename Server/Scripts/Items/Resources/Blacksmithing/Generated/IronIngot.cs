// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class IronIngot : BaseIngot {
		[Constructable]
		public IronIngot() : this( 1 ) {}

		[Constructable]
		public IronIngot( int amount ) : base( CraftResource.Iron, amount ) {
			this.Name = "iron ingot";
			this.Hue = 0x0;
		}

		public IronIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "iron ingot"; } }
		public string ResourceName { get { return "iron"; } }
		public double ResourceQuality { get { return 1.00; } }

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
