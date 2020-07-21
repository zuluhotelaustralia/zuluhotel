using System;

namespace Server.Items
{
    [FlipableAttribute( 0xF50, 0xF4F )]
	public class Crossbow : BaseRanged
	{
		public override int EffectID{ get{ return 0x1BFE; } }
		public override Type AmmoType{ get{ return typeof( Bolt ); } }
		public override Item Ammo{ get{ return new Bolt(); } }

		public override int DefaultStrengthReq{ get{ return 30; } }
		public override int DefaultMinDamage{ get{ return 8; } }
		public override int DefaultMaxDamage{ get{ return 43; } }
		public override int DefaultSpeed{ get{ return 18; } }

		public override int DefMaxRange{ get{ return 8; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 80; } }


		[Constructible]
public Crossbow() : base( 0xF50 )
		{
			Weight = 7.0;
			Layer = Layer.TwoHanded;
		}

		[Constructible]
public Crossbow( Serial serial ) : base( serial )
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
