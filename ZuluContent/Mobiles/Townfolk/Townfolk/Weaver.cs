using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Weaver : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.TailorsGuild; } }


		[Constructible]
public Weaver() : base( "the weaver" )
		{
			SetSkill( SkillName.Tailoring, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBWeaver() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return VendorShoeType.Sandals; }
		}

		[Constructible]
public Weaver( Serial serial ) : base( serial )
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
