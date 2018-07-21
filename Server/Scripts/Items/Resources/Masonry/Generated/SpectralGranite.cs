// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class SpectralGranite : BaseGranite {
		[Constructable]
		public SpectralGranite() : base( CraftResource.Spectral ) {
			this.Name = "spectral granite";
			this.Hue = 0x483;
		}

		public SpectralGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "spectral"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Spectral Granite");
		}
	}	
}
