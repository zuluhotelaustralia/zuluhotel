namespace Server.Items
{
    [FlipableAttribute( 0x144e, 0x1453 )]
	public class BoneArms : BaseArmor
	{
		public override int InitMinHits{ get{ return 25; } }
		public override int InitMaxHits{ get{ return 30; } }

		public override int DefaultStrReq{ get{ return 40; } }

		public override int DefaultDexBonus{ get{ return -2; } }

		public override int ArmorBase{ get{ return 30; } }
		public override int RevertArmorBase{ get{ return 4; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }


		[Constructible]
public BoneArms() : base( 0x144E )
		{
			Weight = 2.0;
		}

		[Constructible]
public BoneArms( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			if ( Weight == 1.0 )
				Weight = 2.0;
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
