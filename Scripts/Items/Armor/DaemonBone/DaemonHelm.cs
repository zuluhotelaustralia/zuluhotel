namespace Server.Items
{
    [FlipableAttribute( 0x1451, 0x1456 )]
	public class DaemonHelm : BaseArmor
	{
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override int DefaultStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 46; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override int LabelNumber{ get{ return 1041374; } } // daemon bone helmet

		[Constructable]
		public DaemonHelm() : base( 0x1451 )
		{
			Hue = 0x648;
			Weight = 3.0;
		}

		public DaemonHelm( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 3.0;
		}
	}
}