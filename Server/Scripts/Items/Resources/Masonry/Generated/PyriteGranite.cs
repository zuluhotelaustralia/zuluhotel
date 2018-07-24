// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class PyriteGranite : BaseGranite {
		[Constructable]
		public PyriteGranite() : base( CraftResource.Pyrite ) {
			this.Hue = 0x6b8;
		}

		public PyriteGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "pyrite"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Pyrite Granite");
		}
	}	
}
