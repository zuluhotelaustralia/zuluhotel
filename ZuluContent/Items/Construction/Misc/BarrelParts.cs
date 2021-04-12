namespace Server.Items
{
    public class BarrelLid : Item
	{

		[Constructible]
public BarrelLid() : base(0x1DB8)
		{
			Weight = 2;
		}

		[Constructible]
public BarrelLid(Serial serial) : base(serial)
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

	[FlipableAttribute(0x1EB1, 0x1EB2, 0x1EB3, 0x1EB4)]
	public class BarrelStaves : Item
	{

		public BarrelStaves() : base(0x1EB1)
		{
			Weight = 1;
		}

		public BarrelStaves(Serial serial) : base(serial)
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

	public class BarrelHoops : BaseTinkerItem
	{
		public override int LabelNumber { get { return 1011228; } } // Barrel hoops


		public BarrelHoops() : base(0x1DB7)
		{
			Weight = 5;
		}

		public BarrelHoops(Serial serial) : base(serial)
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

	public class BarrelTap : BaseTinkerItem
	{

		public BarrelTap() : base(0x1004)
		{
			Weight = 1;
		}

		public BarrelTap(Serial serial) : base(serial)
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
