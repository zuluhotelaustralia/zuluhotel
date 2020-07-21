namespace Server.Items
{
    [FlipableAttribute( 0x1BD7, 0x1BDA )]
	public class Board : Item
	{
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get { return m_Resource; }
			set { m_Resource = value; }
		}


		[Constructible]
public Board()
			: this( 1 )
		{
		}


		[Constructible]
public Board( int amount )
			: this( CraftResource.RegularWood, amount )
		{
		}

		[Constructible]
public Board( Serial serial )
			: base( serial )
		{
		}


		[Constructible]
public Board( CraftResource resource ) : this( resource, 1 )
		{
		}


		[Constructible]
public Board( CraftResource resource, int amount )
			: base( 0x1BD7 )
		{
			Stackable = true;
			Amount = amount;

			m_Resource = resource;
			Hue = CraftResources.GetHue( resource );
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 );

			writer.Write( (int)m_Resource );
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 3:
				case 2:
					{
						m_Resource = (CraftResource)reader.ReadInt();
						break;
					}
			}

			if ( version == 0 && Weight == 0.1 || version <= 2 && Weight == 2 )
				Weight = -1;

			if ( version <= 1 )
				m_Resource = CraftResource.RegularWood;
		}
	}
}
