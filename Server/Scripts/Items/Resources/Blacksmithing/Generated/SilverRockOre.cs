// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class SilverRockOre : BaseOre {
		[Constructable]
		public SilverRockOre() : this( 1 ) {}

		[Constructable]
		public SilverRockOre( int amount ) : base( CraftResource.SilverRock, amount ) {
			this.Hue = 0x3e9;
		}

		public SilverRockOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Silver Rock Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new SilverRockIngot();
		}
	}	
}
