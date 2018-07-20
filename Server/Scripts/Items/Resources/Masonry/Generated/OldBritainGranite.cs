// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class OldBritainGranite : BaseGranite {
		[Constructable]
		public OldBritainGranite() : base( CraftResource.OldBritain ) {
			this.Name = "old britain granite";
			this.Hue = 0x852;
		}

		public OldBritainGranite( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Old Britain Granite");
		}
	}	
}
