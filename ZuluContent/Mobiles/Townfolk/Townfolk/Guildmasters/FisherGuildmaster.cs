namespace Server.Mobiles
{
    public class FisherGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.FishermensGuild; } }


		[Constructible]
public FisherGuildmaster() : base( "fisher" )
		{
			SetSkill( SkillName.Fishing, 80.0, 100.0 );
		}

		[Constructible]
public FisherGuildmaster( Serial serial ) : base( serial )
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
