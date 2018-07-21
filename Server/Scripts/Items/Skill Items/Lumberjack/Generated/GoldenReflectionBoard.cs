// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	public class GoldenReflectionBoard : Board {
		[Constructable]
		public GoldenReflectionBoard() : this( 1 ) {}

		[Constructable]
		public GoldenReflectionBoard( int amount ) : base( CraftResource.GoldenReflection, amount ) {
			this.Name = "golden reflection board";
			this.Hue = 48;
		}

		public string ResourceName { get { return "golden reflection"; } }
		public double ResourceQuality { get { return 1.25; } }

		public GoldenReflectionBoard( Serial serial ) : base( serial ) {}

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
