namespace Server.Items{
	[FlipableAttribute( 0x1081, 0x1082 )]
	public class SerpentLeather : BaseLeather
	{
		[Constructable]
		public SerpentLeather() : this( 1 )
		{
		}

		[Constructable]
		public SerpentLeather( int amount ) : base( CraftResource.SerpentLeather, amount )
		{
			this.Hue = 0x8fd;
		}

		public SerpentLeather( Serial serial ) : base( serial )
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
