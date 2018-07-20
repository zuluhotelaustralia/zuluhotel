// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class OldBritainOre : BaseOre {
		[Constructable]
		public OldBritainOre() : this( 1 ) {}

		[Constructable]
		public OldBritainOre( int amount ) : base( CraftResource.OldBritain, amount ) {
			this.Name = "old britain ore";
			this.Hue = 0x852;
		}

		public OldBritainOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Old Britain Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new OldBritainIngot();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Old Britain Ore");
		}
	}	
}
