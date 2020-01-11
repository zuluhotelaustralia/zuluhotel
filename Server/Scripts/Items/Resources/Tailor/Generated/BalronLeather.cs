namespace Server.Items{
	[FlipableAttribute( 0x1081, 0x1082 )]
	public class BalronLeather : BaseLeather
	{
		[Constructable]
		public BalronLeather() : this( 1 )
		{
		}

		[Constructable]
		public BalronLeather( int amount ) : base( CraftResource.BalronLeather, amount )
		{
			this.Hue = 1175;
		}

		public BalronLeather( Serial serial ) : base( serial )
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
