// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items {

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class NewZuluIngot : BaseIngot {
		[Constructable]
		public NewZuluIngot() : this( 1 ) {}

		[Constructable]
		public NewZuluIngot( int amount ) : base( CraftResource.NewZulu, amount ) {
			this.Name = "new zulu ingot";
			this.Hue = 0x488;
		}

		public NewZuluIngot( Serial serial ) : base( serial ) {}

		public override string DefaultName { get { return "new zulu ingot"; } }

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
