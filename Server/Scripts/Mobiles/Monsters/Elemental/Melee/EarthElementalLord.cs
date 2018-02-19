\using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an earth elemental corpse" )]
	public class EarthElementalLord : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 130.0; } }
		public override double DispelFocus{ get{ return 90.0; } }

		[Constructable]
		public EarthElementalLord() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an earth elemental lord";
			Body = 14;
			BaseSoundID = 268;

			SetStr( 175, 225 );
			SetDex( 175, 225 );
			SetInt( 100, 125 );

			SetDamage( 16, 25 );

			SetSkill( SkillName.MagicResist, 89.1, 125.0 );
			SetSkill( SkillName.Tactics, 80.1, 120.0 );
			SetSkill( SkillName.Wrestling, 80.1, 120.0 );

			Fame = 7000;
			Karma = -3500;

			VirtualArmor = 44;
			ControlSlots = 2;

			PackItem( new FertileDirt( Utility.RandomMinMax( 1, 4 ) ) );
			PackItem( new MandrakeRoot() );
			
			Item ore = new IronOre( 5 );
			ore.ItemID = 0x19B7;
			PackItem( ore );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 3; } }

		public EarthElementalLord( Serial serial ) : base( serial )
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
		}
	}
}
