using Server.Engines.Harvest;

namespace Server.Items
{
    public class GargoylesPickaxe : BaseAxe, IUsesRemaining
	{
		public override int LabelNumber{ get{ return 1041281; } } // a gargoyle's pickaxe
		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		public override int DefaultStrengthReq{ get{ return 25; } }
		public override int DefaultMinDamage{ get{ return 1; } }
		public override int DefaultMaxDamage{ get{ return 15; } }
		public override int DefaultSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override WeaponAnimation DefaultAnimation{ get{ return WeaponAnimation.Slash1H; } }


		[Constructible]
public GargoylesPickaxe() : this( Utility.RandomMinMax( 101, 125 ))
		{
		}


		[Constructible]
public GargoylesPickaxe( int uses ) : base( 0xE85 + Utility.Random( 2 ))
		{
			Weight = 11.0;
			UsesRemaining = uses;
			ShowUsesRemaining = true;
		}

		[Constructible]
public GargoylesPickaxe( Serial serial ) : base( serial )
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

			if ( Hue == 0x973 )
				Hue = 0x0;
		}
	}
}
