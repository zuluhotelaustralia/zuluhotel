// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class SpikeOre : BaseOre {
		[Constructable]
		public SpikeOre() : this( 1 ) {}

		[Constructable]
		public SpikeOre( int amount ) : base( CraftResource.Spike, amount ) {
			this.Name = "spike ore";
			this.Hue = 0x4c7;
		}

		public SpikeOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Spike Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new SpikeIngot();
		}
	}	
}
