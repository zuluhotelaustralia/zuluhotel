using System;

namespace Server.Items
{
    [FlipableAttribute( 0x13FD, 0x13FC )]
	public class HeavyCrossbow : BaseRanged
	{
		public override int EffectID{ get{ return 0x1BFE; } }
		public override Type AmmoType{ get{ return typeof( Bolt ); } }
		public override Item Ammo{ get{ return new Bolt(); } }

		public override int DefaultStrengthReq{ get{ return 40; } }
		public override int DefaultMinDamage{ get{ return 11; } }
		public override int DefaultMaxDamage{ get{ return 56; } }
		public override int DefaultSpeed{ get{ return 10; } }

		public override int DefaultMaxRange{ get{ return 8; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 100; } }


		[Constructible]
        public HeavyCrossbow() : base( 0x13FD )
		{
			Weight = 9.0;
			Layer = Layer.TwoHanded;
		}

		[Constructible]
        public HeavyCrossbow( Serial serial ) : base( serial )
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
