using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Herbalist : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.MagesGuild; } }


		[Constructible]
public Herbalist() : base( "the herbalist" )
		{
			SetSkill( SkillName.Alchemy, 80.0, 100.0 );
			SetSkill( SkillName.Cooking, 80.0, 100.0 );
			SetSkill( SkillName.TasteID, 80.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBHerbalist() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; }
		}

		[Constructible]
public Herbalist( Serial serial ) : base( serial )
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
