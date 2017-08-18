// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class MalachiteIngot : BaseIngot {
		[Constructable]
		public MalachiteIngot() : this( 1 ) {}

		[Constructable]
		public MalachiteIngot( int amount ) : base( CraftResource.Malachite, amount ) {}

		public MalachiteIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Malachite Ingot"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Malachite Ingot");
		}
	}	
}
