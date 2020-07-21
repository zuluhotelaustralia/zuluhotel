using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Architect : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.TinkersGuild; } }


		[Constructible]
public Architect() : base( "the architect" )
		{
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBHouseDeed() );

			m_SBInfos.Add( new SBArchitect() );
		}

		[Constructible]
public Architect( Serial serial ) : base( serial )
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
