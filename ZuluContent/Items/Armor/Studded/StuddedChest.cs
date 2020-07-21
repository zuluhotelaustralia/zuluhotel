namespace Server.Items
{
    [FlipableAttribute( 0x13db, 0x13e2 )]
	public class StuddedChest : BaseArmor
	{
		public override int InitMinHits{ get{ return 35; } }
		public override int InitMaxHits{ get{ return 45; } }

		public override int DefaultStrReq{ get{ return 35; } }

		public override int ArmorBase{ get{ return 16; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Studded; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.Half; } }


		[Constructible]
public StuddedChest() : base( 0x13DB )
		{
			Weight = 8.0;
		}

		[Constructible]
public StuddedChest( Serial serial ) : base( serial )
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
				Weight = 8.0;
		}
	}
}
