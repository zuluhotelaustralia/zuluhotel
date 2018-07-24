// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class PeachblueOre : BaseOre {
		[Constructable]
		public PeachblueOre() : this( 1 ) {}

		[Constructable]
		public PeachblueOre( int amount ) : base( CraftResource.Peachblue, amount ) {
			this.Hue = 0x49c;
		}

		public PeachblueOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Peachblue Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new PeachblueIngot();
		}
	}	
}
