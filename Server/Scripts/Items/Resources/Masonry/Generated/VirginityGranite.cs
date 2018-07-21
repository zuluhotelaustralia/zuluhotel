// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class VirginityGranite : BaseGranite {
		[Constructable]
		public VirginityGranite() : base( CraftResource.Virginity ) {
			this.Name = "virginity granite";
			this.Hue = 0x482;
		}

		public VirginityGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "virginity"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Virginity Granite");
		}
	}	
}
