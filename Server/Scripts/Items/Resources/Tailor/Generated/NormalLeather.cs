namespace Server.Items{
	[FlipableAttribute( 0x1081, 0x1082 )]
	public class NormalLeather : BaseLeather
	{
		[Constructable]
		public NormalLeather() : this( 1 )
		{
		}

		[Constructable]
		public NormalLeather( int amount ) : base( CraftResource.NormalLeather, amount )
		{
			this.Hue = 0;
		}

		public NormalLeather( Serial serial ) : base( serial )
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
