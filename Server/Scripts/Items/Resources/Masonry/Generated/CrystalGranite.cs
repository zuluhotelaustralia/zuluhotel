// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class CrystalGranite : BaseGranite {
		[Constructable]
		public CrystalGranite() : base( CraftResource.Crystal ) {
			this.Name = "crystal granite";
			this.Hue = 0x492;
		}

		public CrystalGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "crystal"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Crystal Granite");
		}
	}	
}
