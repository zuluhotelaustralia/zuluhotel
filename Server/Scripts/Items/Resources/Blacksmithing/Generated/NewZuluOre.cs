// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class NewZuluOre : BaseOre {
		[Constructable]
		public NewZuluOre() : this( 1 ) {}

		[Constructable]
		public NewZuluOre( int amount ) : base( CraftResource.NewZulu, amount ) {
			this.Hue = 0x488;
		}

		public NewZuluOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "New Zulu Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new NewZuluIngot();
		}
	}	
}
