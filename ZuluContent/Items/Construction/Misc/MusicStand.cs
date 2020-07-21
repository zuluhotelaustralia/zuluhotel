namespace Server.Items
{
    [Furniture]
	[Flipable(0xEBB, 0xEBC)]
	public class TallMusicStand : Item
	{

		[Constructible]
public TallMusicStand() : base(0xEBB)
		{
			Weight = 10.0;
		}

		[Constructible]
public TallMusicStand(Serial serial) : base(serial)
		{
		}

		public override void Serialize(IGenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 8.0 )
				Weight = 10.0;
		}
	}

	[Furniture]
	[Flipable(0xEB6,0xEB8)]
	public class ShortMusicStand : Item
	{

		public ShortMusicStand() : base(0xEB6)
		{
			Weight = 10.0;
		}

		public ShortMusicStand(Serial serial) : base(serial)
		{
		}

		public override void Serialize(IGenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 10.0;
		}
	}
}
