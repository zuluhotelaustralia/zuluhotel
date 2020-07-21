namespace Server.Items
{
	class ClosedBarrel : TrapableContainer
	{
		public override int DefaultGumpID{ get { return 0x3e; } }


		[Constructible]
public ClosedBarrel()
			: base( 0x0FAE )
		{
		}

		[Constructible]
public ClosedBarrel( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
