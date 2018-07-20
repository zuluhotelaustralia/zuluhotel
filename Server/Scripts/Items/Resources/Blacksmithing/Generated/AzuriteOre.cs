// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class AzuriteOre : BaseOre {
		[Constructable]
		public AzuriteOre() : this( 1 ) {}

		[Constructable]
		public AzuriteOre( int amount ) : base( CraftResource.Azurite, amount ) {
			this.Name = "azurite ore";
			this.Hue = 0x4df;
		}

		public AzuriteOre( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "Azurite Ore"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot() {
			return new AzuriteIngot();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Azurite Ore");
		}
	}	
}
