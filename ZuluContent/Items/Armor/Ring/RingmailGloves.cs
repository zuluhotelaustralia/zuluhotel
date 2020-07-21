namespace Server.Items
{
    [FlipableAttribute( 0x13eb, 0x13f2 )]
	public class RingmailGloves : BaseArmor
	{
		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override int DefaultStrReq{ get{ return 20; } }

		public override int DefaultDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 22; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Ringmail; } }


		[Constructible]
public RingmailGloves() : base( 0x13EB )
		{
			Weight = 2.0;
		}

		[Constructible]
public RingmailGloves( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}
