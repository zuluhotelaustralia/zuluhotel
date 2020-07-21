namespace Server.Items
{
    public class TotalRefreshPotion : BaseRefreshPotion
	{
		public override double Refresh{ get{ return 1.0; } }


		[Constructible]
public TotalRefreshPotion() : base( PotionEffect.RefreshTotal )
		{
		}

		[Constructible]
public TotalRefreshPotion( Serial serial ) : base( serial )
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
