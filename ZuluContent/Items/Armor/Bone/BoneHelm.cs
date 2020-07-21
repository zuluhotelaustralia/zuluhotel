namespace Server.Items
{
    [FlipableAttribute( 0x1451, 0x1456 )]
	public class BoneHelm : BaseArmor
	{
		public override int InitMinHits{ get{ return 25; } }
		public override int InitMaxHits{ get{ return 30; } }

		public override int DefaultStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 30; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }


		[Constructible]
public BoneHelm() : base( 0x1451 )
		{
			Weight = 3.0;
		}

		[Constructible]
public BoneHelm( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			if ( Weight == 1.0 )
				Weight = 3.0;
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
