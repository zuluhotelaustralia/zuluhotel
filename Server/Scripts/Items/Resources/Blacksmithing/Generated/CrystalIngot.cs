// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class CrystalIngot : BaseIngot {
		[Constructable]
		public CrystalIngot() : this( 1 ) {}

		[Constructable]
		public CrystalIngot( int amount ) : base( CraftResource.Crystal, amount ) {
			this.Name = "crystal ingot";
			this.Hue = 0x492;
		}

		public CrystalIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "crystal ingot"; } }

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
