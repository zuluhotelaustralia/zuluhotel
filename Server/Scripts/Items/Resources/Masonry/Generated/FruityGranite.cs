// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class FruityGranite : BaseGranite {
		[Constructable]
		public FruityGranite() : base( CraftResource.Fruity ) {}

		public FruityGranite( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Fruity Granite");
		}
	}	
}
