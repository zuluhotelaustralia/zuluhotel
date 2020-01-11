namespace Server.Items{
	[FlipableAttribute( 0x1081, 0x1082 )]
	public class GoldenDragonLeather : BaseLeather
	{
		[Constructable]
		public GoldenDragonLeather() : this( 1 )
		{
		}

		[Constructable]
		public GoldenDragonLeather( int amount ) : base( CraftResource.GoldenDragonLeather, amount )
		{
			this.Hue = 48;
		}

		public GoldenDragonLeather( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
