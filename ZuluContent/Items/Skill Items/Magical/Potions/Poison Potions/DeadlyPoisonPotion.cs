namespace Server.Items
{
    public class DeadlyPoisonPotion : BasePoisonPotion
	{
		public override Poison Poison{ get{ return Poison.Deadly; } }

		public override double MinPoisoningSkill{ get{ return 95.0; } }
		public override double MaxPoisoningSkill{ get{ return 100.0; } }


		[Constructible]
public DeadlyPoisonPotion() : base( PotionEffect.PoisonDeadly )
		{
		}

		[Constructible]
public DeadlyPoisonPotion( Serial serial ) : base( serial )
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
