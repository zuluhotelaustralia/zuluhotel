// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class GoldGranite : BaseGranite {
		[Constructable]
		public GoldGranite() : base( CraftResource.Gold ) {
			this.Hue = 2793;
		}

		public GoldGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "gold"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Gold Granite");
		}
	}	
}
