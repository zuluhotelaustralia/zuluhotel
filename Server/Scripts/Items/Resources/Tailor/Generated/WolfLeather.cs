namespace Server.Items{
	[FlipableAttribute( 0x1081, 0x1082 )]
	public class WolfLeather : BaseLeather
	{
		[Constructable]
		public WolfLeather() : this( 1 )
		{
		}

		[Constructable]
		public WolfLeather( int amount ) : base( CraftResource.WolfLeather, amount )
		{
			this.Hue = 1102;
		}

		public WolfLeather( Serial serial ) : base( serial )
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
