// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class MalachiteGranite : BaseGranite {
		[Constructable]
		public MalachiteGranite() : base( CraftResource.Malachite ) {
			this.Name = "malachite granite";
			this.Hue = 0x487;
		}

		public MalachiteGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "malachite"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Malachite Granite");
		}
	}	
}
