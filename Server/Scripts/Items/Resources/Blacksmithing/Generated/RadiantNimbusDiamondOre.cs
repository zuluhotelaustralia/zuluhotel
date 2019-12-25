// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class RadiantNimbusDiamondOre : BaseOre {
		[Constructable]
		public RadiantNimbusDiamondOre() : this( 1 ) {}

		[Constructable]
		public RadiantNimbusDiamondOre( int amount ) : base( CraftResource.RadiantNimbusDiamond, amount ) {
			this.Hue = 2765;
		}

		public RadiantNimbusDiamondOre( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new RadiantNimbusDiamondIngot();
		}
	}	
}
