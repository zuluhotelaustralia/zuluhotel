// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class AzuriteIngot : BaseIngot {
		[Constructable]
		public AzuriteIngot() : this( 1 ) {}

		[Constructable]
		public AzuriteIngot( int amount ) : base( CraftResource.Azurite, amount ) {
			this.Hue = 0x4df;
		}

		public AzuriteIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "azurite ingot"; } }
		public string ResourceName { get { return "azurite"; } }
		public double ResourceQuality { get { return 1.50; } }

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
