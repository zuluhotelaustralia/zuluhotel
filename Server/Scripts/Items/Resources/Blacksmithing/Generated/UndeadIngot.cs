// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class UndeadIngot : BaseIngot {
		[Constructable]
		public UndeadIngot() : this( 1 ) {}

		[Constructable]
		public UndeadIngot( int amount ) : base( CraftResource.Undead, amount ) {
			this.Hue = 0x279;
		}

		public UndeadIngot( Serial serial ) : base( serial ) {}

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
