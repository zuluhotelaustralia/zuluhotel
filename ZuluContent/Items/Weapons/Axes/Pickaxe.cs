using Server.Engines.Harvest;

namespace Server.Items
{
    [FlipableAttribute( 0xE86, 0xE85 )]
	public class Pickaxe : BaseAxe, IUsesRemaining
	{
		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		public override int DefaultStrengthReq{ get{ return 25; } }
		public override int DefaultMinDamage{ get{ return 1; } }
		public override int DefaultMaxDamage{ get{ return 15; } }
		public override int DefaultSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override WeaponAnimation DefaultAnimation{ get{ return WeaponAnimation.Slash1H; } }


		[Constructible]
public Pickaxe() : base( 0xE86 )
		{
			Weight = 11.0;
			UsesRemaining = 50;
			ShowUsesRemaining = true;
		}

		[Constructible]
public Pickaxe( Serial serial ) : base( serial )
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
			ShowUsesRemaining = true;
		}
	}
}
