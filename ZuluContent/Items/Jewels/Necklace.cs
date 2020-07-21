namespace Server.Items
{
    public abstract class BaseNecklace : BaseJewel
	{
		public override int BaseGemTypeNumber{ get{ return 1044241; } } // star sapphire necklace

		public BaseNecklace( int itemID ) : base( itemID, Layer.Neck )
		{
		}

		public BaseNecklace( Serial serial ) : base( serial )
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

	public class Necklace : BaseNecklace
	{

		[Constructible]
public Necklace() : base( 0x1085 )
		{
			Weight = 0.1;
		}

		[Constructible]
public Necklace( Serial serial ) : base( serial )
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

	public class GoldNecklace : BaseNecklace
	{

		public GoldNecklace() : base( 0x1088 )
		{
			Weight = 0.1;
		}

		public GoldNecklace( Serial serial ) : base( serial )
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

	public class GoldBeadNecklace : BaseNecklace
	{

		public GoldBeadNecklace() : base( 0x1089 )
		{
			Weight = 0.1;
		}

		public GoldBeadNecklace( Serial serial ) : base( serial )
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


	public class SilverNecklace : BaseNecklace
	{

		public SilverNecklace() : base( 0x1F08 )
		{
			Weight = 0.1;
		}

		public SilverNecklace( Serial serial ) : base( serial )
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

	public class SilverBeadNecklace : BaseNecklace
	{

		public SilverBeadNecklace() : base( 0x1F05 )
		{
			Weight = 0.1;
		}

		public SilverBeadNecklace( Serial serial ) : base( serial )
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
