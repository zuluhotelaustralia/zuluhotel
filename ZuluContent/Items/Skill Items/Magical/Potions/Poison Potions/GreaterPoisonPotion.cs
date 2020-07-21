namespace Server.Items
{
    public class GreaterPoisonPotion : BasePoisonPotion
	{
		public override Poison Poison{ get{ return Poison.Greater; } }

		public override double MinPoisoningSkill{ get{ return 60.0; } }
		public override double MaxPoisoningSkill{ get{ return 100.0; } }


		[Constructible]
public GreaterPoisonPotion() : base( PotionEffect.PoisonGreater )
		{
		}

		[Constructible]
public GreaterPoisonPotion( Serial serial ) : base( serial )
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
