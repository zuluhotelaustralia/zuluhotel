namespace Server.Items
{
    [FlipableAttribute(0xec6, 0xec7)]
	public class Dressform : Item
	{

		[Constructible]
public Dressform() : base(0xec6)
		{
			Weight = 10;
		}

		[Constructible]
public Dressform(Serial serial) : base(serial)
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
		}
	}
}
