// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class PlatinumOre : BaseOre {
		[Constructable]
		public PlatinumOre() : this( 1 ) {}

		[Constructable]
		public PlatinumOre( int amount ) : base( CraftResource.Platinum, amount ) {}

		public PlatinumOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Platinum Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new PlatinumIngot();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Platinum Ore");
		}
	}	
}
