// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class DarkPaganGranite : BaseGranite {
		[Constructable]
		public DarkPaganGranite() : base( CraftResource.DarkPagan ) {
			this.Name = "dark pagan granite";
			this.Hue = 0x46b;
		}

		public DarkPaganGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "dark pagan"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Dark Pagan Granite");
		}
	}	
}
