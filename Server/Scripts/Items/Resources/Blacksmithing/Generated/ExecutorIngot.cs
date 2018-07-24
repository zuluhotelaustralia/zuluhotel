// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class ExecutorIngot : BaseIngot {
		[Constructable]
		public ExecutorIngot() : this( 1 ) {}

		[Constructable]
		public ExecutorIngot( int amount ) : base( CraftResource.Executor, amount ) {
			this.Hue = 0x499;
		}

		public ExecutorIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "executor ingot"; } }
		public string ResourceName { get { return "executor"; } }
		public double ResourceQuality { get { return 1.60; } }

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
