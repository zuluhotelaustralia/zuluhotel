// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class RedElvenIngot : BaseIngot {
		[Constructable]
		public RedElvenIngot() : this( 1 ) {}

		[Constructable]
		public RedElvenIngot( int amount ) : base( CraftResource.RedElven, amount ) {}

		public RedElvenIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Red Elven Ingot"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Red Elven Ingot");
		}
	}	
}
