namespace Server.Items
{
    public class StatueSouth : Item
	{

		[Constructible]
public StatueSouth() : base(0x139A)
		{
			Weight = 10;
		}

		[Constructible]
public StatueSouth(Serial serial) : base(serial)
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

	public class StatueSouth2 : Item
	{

		public StatueSouth2() : base(0x1227)
		{
			Weight = 10;
		}

		public StatueSouth2(Serial serial) : base(serial)
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

	public class StatueNorth : Item
	{

		public StatueNorth() : base(0x139B)
		{
			Weight = 10;
		}

		public StatueNorth(Serial serial) : base(serial)
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

	public class StatueWest : Item
	{

		public StatueWest() : base(0x1226)
		{
			Weight = 10;
		}

		public StatueWest(Serial serial) : base(serial)
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

	public class StatueEast : Item
	{

		public StatueEast() : base(0x139C)
		{
			Weight = 10;
		}

		public StatueEast(Serial serial) : base(serial)
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

	public class StatueEast2 : Item
	{

		public StatueEast2() : base(0x1224)
		{
			Weight = 10;
		}

		public StatueEast2(Serial serial) : base(serial)
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

	public class StatueSouthEast : Item
	{

		public StatueSouthEast() : base(0x1225)
		{
			Weight = 10;
		}

		public StatueSouthEast(Serial serial) : base(serial)
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

	public class BustSouth : Item
	{

		public BustSouth() : base(0x12CB)
		{
			Weight = 10;
		}

		public BustSouth(Serial serial) : base(serial)
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

	public class BustEast : Item
	{

		public BustEast() : base(0x12CA)
		{
			Weight = 10;
		}

		public BustEast(Serial serial) : base(serial)
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

	public class StatuePegasus : Item
	{

		public StatuePegasus() : base(0x139D)
		{
			Weight = 10;
		}

		public StatuePegasus(Serial serial) : base(serial)
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

	public class StatuePegasus2 : Item
	{

		public StatuePegasus2() : base(0x1228)
		{
			Weight = 10;
		}

		public StatuePegasus2(Serial serial) : base(serial)
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

	public class SmallTowerSculpture : Item
	{

		public SmallTowerSculpture() : base(0x241A)
		{
			Weight = 20.0;
		}

		public SmallTowerSculpture(Serial serial) : base(serial)
		{
		}

		public override void Serialize(IGenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write( (int) 0 );
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
