using Server.Items;
using Server.Guilds;

namespace Server.Mobiles
{
    public class OrderGuard : BaseShieldGuard
	{
		public override int Keyword{ get{ return 0x21; } } // *order shield*
		public override BaseShield Shield{ get{ return new OrderShield(); } }
		public override int SignupNumber{ get{ return 1007141; } } // Sign up with a guild of order if thou art interested.
		public override GuildType Type{ get{ return GuildType.Order; } }

		public override bool BardImmune{ get{ return true; } }


		[Constructible]
public OrderGuard()
		{
		}

		[Constructible]
public OrderGuard( Serial serial ) : base( serial )
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
