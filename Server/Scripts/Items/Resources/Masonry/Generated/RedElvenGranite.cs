// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class RedElvenGranite : BaseGranite {
		[Constructable]
		public RedElvenGranite() : base( CraftResource.RedElven ) {
			this.Name = "red elven granite";
			this.Hue = 0x4b9;
		}

		public RedElvenGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "red elven"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Red Elven Granite");
		}
	}	
}
