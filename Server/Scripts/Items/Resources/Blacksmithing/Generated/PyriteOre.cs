// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class PyriteOre : BaseOre {
		[Constructable]
		public PyriteOre() : this( 1 ) {}

		[Constructable]
		public PyriteOre( int amount ) : base( CraftResource.Pyrite, amount ) {
			this.Name = "pyrite ore";
			this.Hue = 0x6b8;
		}

		public PyriteOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Pyrite Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new PyriteIngot();
		}
	}	
}
