// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class SilverRockOre : BaseOre {
		[Constructable]
		public SilverRockOre() : this( 1 ) {}

		[Constructable]
		public SilverRockOre( int amount ) : base( CraftResource.SilverRock, amount ) {}

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

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Silver Rock Ore");
		}
	}	
}
