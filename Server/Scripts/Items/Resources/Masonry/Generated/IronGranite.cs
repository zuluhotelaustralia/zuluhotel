// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class IronGranite : BaseGranite {
		[Constructable]
		public IronGranite() : base( CraftResource.Iron ) {
			this.Name = "iron granite";
			this.Hue = 0x0;
		}

		public IronGranite( Serial serial ) : base( serial ) {}

		public string ResourceName { get { return "iron"; } }

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Iron Granite");
		}
	}	
}
