// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class DoomOre : BaseOre {
		[Constructable]
		public DoomOre() : this( 1 ) {}

		[Constructable]
		public DoomOre( int amount ) : base( CraftResource.Doom, amount ) {
			this.Name = "doom ore";
			this.Hue = 0x49f;
		}

		public DoomOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Doom Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new DoomIngot();
		}
	}	
}
