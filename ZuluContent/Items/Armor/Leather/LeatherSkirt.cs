namespace Server.Items
{
    [FlipableAttribute( 0x1c08, 0x1c09 )]
	public class LeatherSkirt : BaseArmor
	{
    	public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override int DefaultStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 13; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		public override bool AllowMaleWearer{ get{ return false; } }


		[Constructible]
public LeatherSkirt() : base( 0x1C08 )
		{
			Weight = 1.0;
		}

		[Constructible]
public LeatherSkirt( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			if ( Weight == 3.0 )
				Weight = 1.0;
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
