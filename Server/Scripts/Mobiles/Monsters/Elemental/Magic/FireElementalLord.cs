using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a fire elemental corpse" )]
	public class FireElementalLord : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public FireElementalLord () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fire elemental lord";
			Body = 15;
			BaseSoundID = 838;

			SetStr( 175, 225 );
			SetDex( 175, 225 );
			SetInt( 150, 175 );

			SetDamage( 10, 15 );

			SetSkill( SkillName.EvalInt, 90.0, 115.0 );
			SetSkill( SkillName.Magery, 90.0, 115.0 );
			SetSkill( SkillName.MagicResist, 90.0, 115.0 );
			SetSkill( SkillName.Tactics, 90.0, 115.0 );
			SetSkill( SkillName.Wrestling, 90.0, 115.0 );

			Fame = 9000;
			Karma = -4500;

			VirtualArmor = 50;
			ControlSlots = 4;

			PackItem( new SulfurousAsh( 3 ) );

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls );
			AddLoot( LootPack.HighScrolls );
			AddLoot( LootPack.Gems );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }

		public FireElementalLord( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( BaseSoundID == 274 )
				BaseSoundID = 838;
		}
	}
}
