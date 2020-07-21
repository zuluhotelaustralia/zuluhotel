namespace Server.Items
{
    public class Vase : Item
	{

		[Constructible]
public Vase() : base( 0xB46 )
		{
			Weight = 10;
		}

		[Constructible]
public Vase( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LargeVase : Item
	{

		public LargeVase() : base( 0xB45 )
		{
			Weight = 15;
		}

		public LargeVase( Serial serial ) : base(serial)
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SmallUrn : Item
	{

		public SmallUrn() : base( 0x241C )
		{
			Weight = 20.0;
		}

		public SmallUrn(Serial serial) : base(serial)
		{
		}

		public override void Serialize(IGenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write( (int)0 );
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
