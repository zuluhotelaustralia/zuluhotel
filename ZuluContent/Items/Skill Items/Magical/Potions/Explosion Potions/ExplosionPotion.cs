namespace Server.Items
{
    public class ExplosionPotion : BaseExplosionPotion
	{
		public override int MinDamage { get { return 10; } }
		public override int MaxDamage { get { return 20; } }


		[Constructible]
public ExplosionPotion() : base( PotionEffect.Explosion )
		{
		}

		[Constructible]
public ExplosionPotion( Serial serial ) : base( serial )
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
