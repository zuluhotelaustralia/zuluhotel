using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Multis;
using Server.Engines.Craft;

namespace Server.Items
{
    public class Earthbook : Item, ISecurable
	{
        private List<string> m_Entries;
		private string m_Description = "Book of the Earth";
        private int m_DefaultIndex;
        private SecureLevel m_Level;

        private List<Mobile> m_Openers = new List<Mobile>();

        [CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string Description
		{
			get
			{
				return m_Description;
			}
			set
			{
				m_Description = value;
			}
		}

        public List<Mobile> Openers
        {
            get
            {
                return m_Openers;
            }
            set
            {
                m_Openers = value;
            }
        }


        [Constructible]
        public Earthbook() : base( 0x0EFA )
		{
			Weight = 3.0;
			LootType = LootType.Blessed;
			Hue = 0x48A;

			Layer = Layer.OneHanded;

			m_Entries = new List<string>();
            
            m_DefaultIndex = -1;

			m_Level = SecureLevel.CoOwners;
		}


        public List<string> Entries
		{
			get
			{
				return m_Entries;
			}
		}

		public string Default
		{
			get
			{
				if ( m_DefaultIndex >= 0 && m_DefaultIndex < m_Entries.Count )
					return m_Entries[m_DefaultIndex];

				return null;
			}
			set
			{
				if ( value == null )
					m_DefaultIndex = -1;
				else
					m_DefaultIndex = m_Entries.IndexOf( value );
			}
		}

		[Constructible]
        public Earthbook( Serial serial ) : base( serial )
		{
		}

		public override bool AllowEquippedCast( Mobile from )
		{
			return true;
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write(0);

            writer.Write( (int) m_Level );

			writer.Write( m_Entries.Count );

			for ( int i = 0; i < m_Entries.Count; ++i )
				writer.Write(m_Entries[i]);

            writer.Write(m_Description);
            writer.Write( m_DefaultIndex );
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			LootType = LootType.Blessed;

			int version = reader.ReadInt();

			switch ( version )
			{
                case 0:
				{
                    m_Level = (SecureLevel)reader.ReadInt();
                    int count = reader.ReadInt();

					m_Entries = new List<string>( count );

					for ( int i = 0; i < count; ++i )
						m_Entries.Add(reader.ReadString());

					m_Description = reader.ReadString();
                    m_DefaultIndex = reader.ReadInt();

					break;
				}
			}
		}

        public bool IsOpen( Mobile toCheck )
		{
			NetState ns = toCheck.NetState;

			if ( ns != null ) {
				foreach ( Gump gump in ns.Gumps ) {
					EarthbookGump bookGump = gump as EarthbookGump;

					if ( bookGump != null && bookGump.Book == this ) {
						return true;
					}
				}
			}

			return false;
		}

		public override bool DisplayLootType{ get{ return false; } }

		public override bool OnDragLift( Mobile from )
		{
			if ( from.HasGump<EarthbookGump>() )
			{
				from.SendLocalizedMessage( 500169 ); // You cannot pick that up.
				return false;
			}

			foreach ( Mobile m in m_Openers )
				if ( IsOpen( m ) )
					m.CloseGump<EarthbookGump>();;

			m_Openers.Clear();

			return true;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( m_Description != null && m_Description.Length > 0 )
				LabelTo( from, m_Description );
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( GetWorldLocation(), 1 ) && CheckAccess( from ) )
			{
				if ( RootParent is BaseCreature )
				{
					from.SendLocalizedMessage( 502402 ); // That is inaccessible.
					return;
				}

                from.CloseGump<EarthbookGump>();;
				from.SendGump( new EarthbookGump( from, this ) );

				m_Openers.Add( from );
			}
		}

        public override void OnAfterDuped( Item newItem )
		{
			Earthbook book = newItem as Earthbook;

			if ( book == null )
				return;

			book.m_Entries = new List<string>();

			for ( int i = 0; i < m_Entries.Count; i++ )
			{
                string entry = m_Entries[i];

				book.m_Entries.Add(entry);
			}
		}

		public bool CheckAccess( Mobile m )
		{
			if ( !IsLockedDown || m.AccessLevel >= AccessLevel.GameMaster )
				return true;

			BaseHouse house = BaseHouse.FindHouseAt( this );

			return house != null && house.HasSecureAccess( m, m_Level );
		}
    }
}
