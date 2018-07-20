// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class MalachiteOre : BaseOre {
		[Constructable]
		public MalachiteOre() : this( 1 ) {}

		[Constructable]
		public MalachiteOre( int amount ) : base( CraftResource.Malachite, amount ) {
			this.Name = "malachite ore";
			this.Hue = 0x487;
		}

		public MalachiteOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Malachite Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new MalachiteIngot();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Malachite Ore");
		}
	}	
}
