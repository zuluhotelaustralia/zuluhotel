using System;

namespace Server.Items
{
    public class SubtextSign : Sign
	{
		private string m_Subtext;

		[CommandProperty( AccessLevel.GameMaster )]
		public string Subtext
		{
			get { return m_Subtext; }
			set { m_Subtext = value; }
		}


		[Constructible]
public SubtextSign( SignType type, SignFacing facing, string subtext )
			: base( type, facing )
		{
			m_Subtext = subtext;
		}


		[Constructible]
public SubtextSign( int itemID, string subtext )
			: base( itemID )
		{
			m_Subtext = subtext;
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if ( !String.IsNullOrEmpty( m_Subtext ) )
				LabelTo( from, m_Subtext );
		}

		[Constructible]
public SubtextSign( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			writer.Write( m_Subtext );
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Subtext = reader.ReadString();
		}
	}
}
