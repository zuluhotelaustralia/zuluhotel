// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class FruityIngot : BaseIngot {
		[Constructable]
		public FruityIngot() : this( 1 ) {}

		[Constructable]
		public FruityIngot( int amount ) : base( CraftResource.Fruity, amount ) {
			this.Name = "fruity ingot";
			this.Hue = 0x46e;
		}

		public FruityIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "fruity ingot"; } }
		public string ResourceName { get { return "fruity"; } }
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
