namespace Server.Items
{
    public class MediumStoneTableSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new MediumStoneTableSouthDeed(); } }

		public override bool RetainDeedHue{ get{ return true; } }


		[Constructible]
public MediumStoneTableSouthAddon() : this( 0 )
		{
		}


		[Constructible]
public MediumStoneTableSouthAddon( int hue )
		{
			AddComponent( new AddonComponent( 0x1205 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0x1204 ), 1, 0, 0 );
			Hue = hue;
		}

		[Constructible]
public MediumStoneTableSouthAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class MediumStoneTableSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new MediumStoneTableSouthAddon( Hue ); } }
		public override int LabelNumber{ get{ return 1044509; } } // stone table (South)


		public MediumStoneTableSouthDeed()
		{
		}

		public MediumStoneTableSouthDeed( Serial serial ) : base( serial )
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
