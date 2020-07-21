namespace Server.Items
{
    public class StoneFireplaceEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new StoneFireplaceEastDeed(); } }


		[Constructible]
public StoneFireplaceEastAddon()
		{
			AddComponent( new AddonComponent( 0x959 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0x953 ), 0, 1, 0 );
		}

		[Constructible]
public StoneFireplaceEastAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class StoneFireplaceEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new StoneFireplaceEastAddon(); } }
		public override int LabelNumber{ get{ return 1061848; } } // stone fireplace (east)


		public StoneFireplaceEastDeed()
		{
		}

		public StoneFireplaceEastDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
