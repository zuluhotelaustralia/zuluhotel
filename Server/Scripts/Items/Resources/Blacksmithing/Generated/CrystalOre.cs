// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class CrystalOre : BaseOre {
		[Constructable]
		public CrystalOre() : this( 1 ) {}

		[Constructable]
		public CrystalOre( int amount ) : base( CraftResource.Crystal, amount ) {
			this.Name = "crystal ore";
			this.Hue = 0x492;
		}

		public CrystalOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Crystal Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new CrystalIngot();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Crystal Ore");
		}
	}	
}
