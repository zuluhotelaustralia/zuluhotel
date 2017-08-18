// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class LavarockOre : BaseOre {
		[Constructable]
		public LavarockOre() : this( 1 ) {}

		[Constructable]
		public LavarockOre( int amount ) : base( CraftResource.Lavarock, amount ) {}

		public LavarockOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Lavarock Ore"; } }

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

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Lavarock Ore");
		}
	}	
}
