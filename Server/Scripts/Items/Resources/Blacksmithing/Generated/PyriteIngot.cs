// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class PyriteIngot : BaseIngot {
		[Constructable]
		public PyriteIngot() : this( 1 ) {}

		[Constructable]
		public PyriteIngot( int amount ) : base( CraftResource.Pyrite, amount ) {
			this.Name = "pyrite ingot";
			this.Hue = 0x6b8;
		}

		public PyriteIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "pyrite ingot"; } }

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
