namespace Server.Items
{
    public class MeltedWax : Item
	{
		public override int LabelNumber{ get{ return 1016492; } } // melted wax


		[Constructible]
public MeltedWax() : base( 0x122A )
		{
			Movable = false;
			Hue = 0x835;
		}

		[Constructible]
public MeltedWax(Serial serial) : base(serial)
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
