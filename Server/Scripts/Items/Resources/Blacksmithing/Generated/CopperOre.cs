// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class CopperOre : BaseOre {
		[Constructable]
		public CopperOre() : this( 1 ) {}

		[Constructable]
		public CopperOre( int amount ) : base( CraftResource.Copper, amount ) {
			this.Name = "copper ore";
			this.Hue = 0x602;
		}

		public CopperOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Copper Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new CopperIngot();
		}
	}	
}
