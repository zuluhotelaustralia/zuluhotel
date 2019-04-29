// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class DestructionIngot : BaseIngot {
		[Constructable]
		public DestructionIngot() : this( 1 ) {}

		[Constructable]
		public DestructionIngot( int amount ) : base( CraftResource.Destruction, amount ) {
			this.Hue = 2773;
		}

		public DestructionIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "destruction ingot"; } }
		public string ResourceName { get { return "destruction"; } }
		public double ResourceQuality { get { return 1.70; } }

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
