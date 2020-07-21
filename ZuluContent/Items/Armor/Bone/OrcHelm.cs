namespace Server.Items
{
    public class OrcHelm : BaseArmor
	{
		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override int DefaultStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 20; } }

		public override double DefaultWeight { get { return 5; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.None; } }


		[Constructible]
public OrcHelm() : base( 0x1F0B )
		{
		}

		[Constructible]
public OrcHelm( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 );
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if( version == 0 && ( Weight == 1 || Weight == 5 ) )
			{
				Weight = -1;
			}
		}
	}
}
