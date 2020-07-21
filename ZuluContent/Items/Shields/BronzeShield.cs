namespace Server.Items
{
    public class BronzeShield : BaseShield
	{
		public override int InitMinHits{ get{ return 25; } }
		public override int InitMaxHits{ get{ return 30; } }

		public override int ArmorBase{ get{ return 10; } }


		[Constructible]
public BronzeShield() : base( 0x1B72 )
		{
			Weight = 6.0;
		}

		[Constructible]
public BronzeShield( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
}
