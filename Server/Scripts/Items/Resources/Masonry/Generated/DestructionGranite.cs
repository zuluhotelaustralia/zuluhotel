// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class DestructionGranite : BaseGranite {
		[Constructable]
		public DestructionGranite() : base( CraftResource.Destruction ) {
			this.Hue = 2773;
		}

		public DestructionGranite( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from ) {
			from.SendMessage("Destruction Granite");
		}
	}	
}
