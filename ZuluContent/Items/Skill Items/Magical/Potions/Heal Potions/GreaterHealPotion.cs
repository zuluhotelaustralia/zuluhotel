namespace Server.Items
{
    public class GreaterHealPotion : BaseHealPotion
	{
		public override int MinHeal { get { return 9; } }
		public override int MaxHeal { get { return 30; } }
		public override double Delay{ get{ return 10.0; } }


		[Constructible]
public GreaterHealPotion() : base( PotionEffect.HealGreater )
		{
		}

		[Constructible]
public GreaterHealPotion( Serial serial ) : base( serial )
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
