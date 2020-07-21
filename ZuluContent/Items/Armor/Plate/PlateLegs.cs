namespace Server.Items
{
    [FlipableAttribute( 0x1411, 0x141a )]
	public class PlateLegs : BaseArmor
	{
		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int DefaultStrReq{ get{ return 60; } }
		public override int DefaultDexBonus{ get{ return -6; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }


		[Constructible]
public PlateLegs() : base( 0x1411 )
		{
			Weight = 7.0;
		}

		[Constructible]
public PlateLegs( Serial serial ) : base( serial )
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
