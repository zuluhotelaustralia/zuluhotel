namespace Server.Items
{
    [FlipableAttribute( 0x1415, 0x1416 )]
	public class PlateChest : BaseArmor
	{
		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int DefaultStrReq{ get{ return 60; } }

		public override int DefaultDexBonus{ get{ return -8; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }


		[Constructible]
public PlateChest() : base( 0x1415 )
		{
			Weight = 10.0;
		}

		[Constructible]
public PlateChest( Serial serial ) : base( serial )
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
				Weight = 10.0;
		}
	}
}
