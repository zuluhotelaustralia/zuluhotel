namespace Server.Items
{
    public class LocalizedSign : Sign
	{
		private int m_LabelNumber;

		public override int LabelNumber{ get{ return m_LabelNumber; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Number{ get{ return m_LabelNumber; } set{ m_LabelNumber = value; } }


		[Constructible]
public LocalizedSign( SignType type, SignFacing facing, int labelNumber ) : base( 0xB95 + 2 * (int)type + (int)facing )
		{
			m_LabelNumber = labelNumber;
		}


		[Constructible]
public LocalizedSign( int itemID, int labelNumber ) : base( itemID )
		{
			m_LabelNumber = labelNumber;
		}

		[Constructible]
public LocalizedSign( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			writer.Write( m_LabelNumber );
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_LabelNumber = reader.ReadInt();
					break;
				}
			}
		}
	}
}
