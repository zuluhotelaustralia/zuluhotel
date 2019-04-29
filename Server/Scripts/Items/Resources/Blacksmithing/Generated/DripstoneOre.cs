// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class DripstoneOre : BaseOre {
		[Constructable]
		public DripstoneOre() : this( 1 ) {}

		[Constructable]
		public DripstoneOre( int amount ) : base( CraftResource.Dripstone, amount ) {
			this.Hue = 2771;
		}

		public DripstoneOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Dripstone Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new DripstoneIngot();
		}
	}	
}
