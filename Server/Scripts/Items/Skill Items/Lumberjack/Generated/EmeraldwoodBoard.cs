// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class EmeraldwoodBoard : Board {
		[Constructable]
		public EmeraldwoodBoard() : this( 1 ) {}

		[Constructable]
		public EmeraldwoodBoard( int amount ) : base( CraftResource.Emeraldwood, amount ) {
			this.Name = "emerald wood board";
			this.Hue = 1159;
		}

		public EmeraldwoodBoard( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}	
}
