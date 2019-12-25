// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class LavarockOre : BaseOre {
		[Constructable]
		public LavarockOre() : this( 1 ) {}

		[Constructable]
		public LavarockOre( int amount ) : base( CraftResource.Lavarock, amount ) {
			this.Hue = 2747;
		}

		public LavarockOre( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new LavarockIngot();
		}
	}	
}
