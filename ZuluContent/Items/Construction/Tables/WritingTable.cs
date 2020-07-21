namespace Server.Items
{
    [Furniture]
	[Flipable(0xB4A,0xB49, 0xB4B, 0xB4C)]
	public class WritingTable : Item
	{

		[Constructible]
public WritingTable() : base(0xB4A)
		{
			Weight = 1.0;
		}

		[Constructible]
public WritingTable(Serial serial) : base(serial)
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

			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
}
