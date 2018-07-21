// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class PlatinumGranite : BaseGranite {
		[Constructable]
		public PlatinumGranite() : base( CraftResource.Platinum ) {
			this.Name = "platinum granite";
			this.Hue = 0x457;
		}

		public PlatinumGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "platinum"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Platinum Granite");
		}
	}	
}
