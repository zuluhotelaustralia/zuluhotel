namespace Server.Items
{
    public class LesserPoisonPotion : BasePoisonPotion
	{
		public override Poison Poison{ get{ return Poison.Lesser; } }

		public override double MinPoisoningSkill{ get{ return 0.0; } }
		public override double MaxPoisoningSkill{ get{ return 60.0; } }


		[Constructible]
public LesserPoisonPotion() : base( PotionEffect.PoisonLesser )
		{
		}

		[Constructible]
public LesserPoisonPotion( Serial serial ) : base( serial )
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
