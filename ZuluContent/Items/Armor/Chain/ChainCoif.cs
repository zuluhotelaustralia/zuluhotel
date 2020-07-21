namespace Server.Items
{
    [FlipableAttribute( 0x13BB, 0x13C0 )]
	public class ChainCoif : BaseArmor
	{
		public override int InitMinHits{ get{ return 35; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override int DefaultStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 28; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Chainmail; } }


		[Constructible]
public ChainCoif() : base( 0x13BB )
		{
			Weight = 1.0;
		}

		[Constructible]
public ChainCoif( Serial serial ) : base( serial )
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
		}
	}
}
