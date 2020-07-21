namespace Server.Items
{
    public class PoisonPotion : BasePoisonPotion
	{
		public override Poison Poison{ get{ return Poison.Regular; } }

		public override double MinPoisoningSkill{ get{ return 30.0; } }
		public override double MaxPoisoningSkill{ get{ return 70.0; } }


		[Constructible]
public PoisonPotion() : base( PotionEffect.Poison )
		{
		}

		[Constructible]
public PoisonPotion( Serial serial ) : base( serial )
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
