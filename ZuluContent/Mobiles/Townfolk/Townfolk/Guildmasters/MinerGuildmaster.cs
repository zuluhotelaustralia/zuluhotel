namespace Server.Mobiles
{
    public class MinerGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.MinersGuild; } }


		[Constructible]
public MinerGuildmaster() : base( "miner" )
		{
			SetSkill( SkillName.ItemID, 60.0, 83.0 );
			SetSkill( SkillName.Mining, 90.0, 100.0 );
		}

		[Constructible]
public MinerGuildmaster( Serial serial ) : base( serial )
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
