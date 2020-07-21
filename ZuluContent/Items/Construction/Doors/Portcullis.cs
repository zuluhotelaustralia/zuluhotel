namespace Server.Items
{
    public class PortcullisNS : BaseDoor
	{
		public override bool UseChainedFunctionality{ get{ return true; } }


		[Constructible]
public PortcullisNS() : base( 0x6F5, 0x6F5, 0xF0, 0xEF, new Point3D( 0, 0, 20 ) )
		{
		}

		[Constructible]
public PortcullisNS( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class PortcullisEW : BaseDoor
	{
		public override bool UseChainedFunctionality{ get{ return true; } }


		public PortcullisEW() : base( 0x6F6, 0x6F6, 0xF0, 0xEF, new Point3D( 0, 0, 20 ) )
		{
		}

		public PortcullisEW( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
