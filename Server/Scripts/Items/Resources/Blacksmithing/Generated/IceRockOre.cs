// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class IceRockOre : BaseOre {
		[Constructable]
		public IceRockOre() : this( 1 ) {}

		[Constructable]
		public IceRockOre( int amount ) : base( CraftResource.IceRock, amount ) {
			this.Hue = 0x480;
		}

		public IceRockOre( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new IceRockIngot();
		}
	}	
}
