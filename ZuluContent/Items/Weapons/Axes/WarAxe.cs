using Server.Engines.Harvest;

namespace Server.Items
{
    [FlipableAttribute( 0x13B0, 0x13AF )]
	public class WarAxe : BaseAxe
	{
		public override int DefaultStrengthReq{ get{ return 35; } }
		public override int DefaultMinDamage{ get{ return 9; } }
		public override int DefaultMaxDamage{ get{ return 27; } }
		public override int DefaultSpeed{ get{ return 40; } }

		public override int DefaultHitSound{ get{ return 0x233; } }
		public override int DefaultMissSound{ get{ return 0x239; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 80; } }

		public override SkillName DefaultSkill{ get{ return SkillName.Macing; } }
		public override WeaponType DefaultWeaponType{ get{ return WeaponType.Bashing; } }
		public override WeaponAnimation DefaultAnimation{ get{ return WeaponAnimation.Bash1H; } }

		public override HarvestSystem HarvestSystem{ get{ return null; } }


		[Constructible]
public WarAxe() : base( 0x13B0 )
		{
			Weight = 8.0;
		}

		[Constructible]
public WarAxe( Serial serial ) : base( serial )
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
