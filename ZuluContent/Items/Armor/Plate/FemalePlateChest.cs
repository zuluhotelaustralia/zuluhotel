namespace Server.Items
{
    [FlipableAttribute( 0x1c04, 0x1c05 )]
	public class FemalePlateChest : BaseArmor
	{
		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int DefaultStrReq{ get{ return 45; } }

		public override int DefaultDexBonus{ get{ return -5; } }

		public override bool AllowMaleWearer{ get{ return false; } }

		public override int ArmorBase{ get{ return 30; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }


		[Constructible]
public FemalePlateChest() : base( 0x1C04 )
		{
			Weight = 4.0;
		}

		[Constructible]
public FemalePlateChest( Serial serial ) : base( serial )
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
				Weight = 4.0;
		}
	}
}
