// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class OnyxOre : BaseOre {
		[Constructable]
		public OnyxOre() : this( 1 ) {}

		[Constructable]
		public OnyxOre( int amount ) : base( CraftResource.Onyx, amount ) {
			this.Hue = 0x455;
		}

		public OnyxOre( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new OnyxIngot();
		}
	}	
}
