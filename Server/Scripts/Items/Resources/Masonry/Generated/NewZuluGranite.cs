// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class NewZuluGranite : BaseGranite {
		[Constructable]
		public NewZuluGranite() : base( CraftResource.NewZulu ) {
			this.Name = "new zulu granite";
			this.Hue = 0x488;
		}

		public NewZuluGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "new zulu"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("New Zulu Granite");
		}
	}	
}
