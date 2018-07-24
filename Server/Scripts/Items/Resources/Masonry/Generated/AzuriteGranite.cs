// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class AzuriteGranite : BaseGranite {
		[Constructable]
		public AzuriteGranite() : base( CraftResource.Azurite ) {
			this.Hue = 0x4df;
		}

		public AzuriteGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "azurite"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Azurite Granite");
		}
	}	
}
