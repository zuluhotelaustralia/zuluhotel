namespace Server.Items
{
    public class GreaterExplosionPotion : BaseExplosionPotion
	{
		public override int MinDamage { get { return 15; } }
		public override int MaxDamage { get { return 30; } }


		[Constructible]
public GreaterExplosionPotion() : base( PotionEffect.ExplosionGreater )
		{
		}

		[Constructible]
public GreaterExplosionPotion( Serial serial ) : base( serial )
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
