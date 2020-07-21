namespace Server.Items
{
    public class Obelisk : Item
	{
		public override int LabelNumber{ get{ return 1016474; } } // an obelisk


		[Constructible]
public Obelisk() : base(0x1184)
		{
			Movable = false;
		}

		[Constructible]
public Obelisk(Serial serial) : base(serial)
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
