// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class BronzeGranite : BaseGranite {
		[Constructable]
		public BronzeGranite() : base( CraftResource.Bronze ) {
			this.Name = "bronze granite";
			this.Hue = 0x45e;
		}

		public BronzeGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "bronze"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Bronze Granite");
		}
	}	
}
