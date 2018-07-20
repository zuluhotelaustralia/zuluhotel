// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class IceRockGranite : BaseGranite {
		[Constructable]
		public IceRockGranite() : base( CraftResource.IceRock ) {
			this.Name = "ice rock granite";
			this.Hue = 0x480;
		}

		public IceRockGranite( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Ice Rock Granite");
		}
	}	
}
