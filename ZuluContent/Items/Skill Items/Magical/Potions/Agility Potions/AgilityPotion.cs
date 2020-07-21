using System;

namespace Server.Items
{
    public class AgilityPotion : BaseAgilityPotion
	{
		public override int DexOffset{ get{ return 10; } }
		public override TimeSpan Duration{ get{ return TimeSpan.FromMinutes( 2.0 ); } }


		[Constructible]
public AgilityPotion() : base( PotionEffect.Agility )
		{
		}

		[Constructible]
public AgilityPotion( Serial serial ) : base( serial )
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
