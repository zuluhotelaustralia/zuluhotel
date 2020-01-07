namespace Server.Items{
	[FlipableAttribute( 0x1081, 0x1082 )]
	public class DragonLeather : BaseLeather
	{
		[Constructable]
		public DragonLeather() : this( 1 )
		{
		}

		[Constructable]
		public DragonLeather( int amount ) : base( CraftResource.DragonLeather, amount )
		{
			this.Hue = 0x494;
		}

		public DragonLeather( Serial serial ) : base( serial )
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
