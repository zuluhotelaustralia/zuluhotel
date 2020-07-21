namespace Server.Items
{
    public class Bascinet : BaseArmor
	{
		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override int DefaultStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 18; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }


		[Constructible]
public Bascinet() : base( 0x140C )
		{
			Weight = 5.0;
		}

		[Constructible]
public Bascinet( Serial serial ) : base( serial )
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
				Weight = 5.0;
		}
	}
}
