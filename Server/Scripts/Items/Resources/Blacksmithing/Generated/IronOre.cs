// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class IronOre : BaseOre {
		[Constructable]
		public IronOre() : this( 1 ) {}

		[Constructable]
		public IronOre( int amount ) : base( CraftResource.Iron, amount ) {}

		public IronOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Iron Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new IronIngot();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Iron Ore");
		}
	}	
}
