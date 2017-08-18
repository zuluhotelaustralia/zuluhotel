// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class PlatinumIngot : BaseIngot {
		[Constructable]
		public PlatinumIngot() : this( 1 ) {}

		[Constructable]
		public PlatinumIngot( int amount ) : base( CraftResource.Platinum, amount ) {}

		public PlatinumIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Platinum Ingot"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Platinum Ingot");
		}
	}	
}
