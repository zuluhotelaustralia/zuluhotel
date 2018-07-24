// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class VirginityOre : BaseOre {
		[Constructable]
		public VirginityOre() : this( 1 ) {}

		[Constructable]
		public VirginityOre( int amount ) : base( CraftResource.Virginity, amount ) {
			this.Hue = 0x482;
		}

		public VirginityOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Virginity Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new VirginityIngot();
		}
	}	
}
