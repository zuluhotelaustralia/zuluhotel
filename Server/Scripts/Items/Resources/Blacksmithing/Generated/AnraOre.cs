// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class AnraOre : BaseOre {
		[Constructable]
		public AnraOre() : this( 1 ) {}

		[Constructable]
		public AnraOre( int amount ) : base( CraftResource.Anra, amount ) {
			this.Hue = 0x48b;
		}

		public AnraOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Anra Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new AnraIngot();
		}
	}	
}
