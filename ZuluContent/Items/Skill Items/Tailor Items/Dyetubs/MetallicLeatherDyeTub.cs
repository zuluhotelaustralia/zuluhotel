namespace Server.Items
{
    public class MetallicLeatherDyeTub : LeatherDyeTub
	{
		public override CustomHuePicker CustomHuePicker{ get{ return null; } }

		public override int LabelNumber { get { return 1153495; } } // Metallic Leather ...

		public override bool MetallicHues { get { return true; } }


		[Constructible]
public MetallicLeatherDyeTub()
		{
			LootType = LootType.Blessed;
		}

		[Constructible]
public MetallicLeatherDyeTub( Serial serial )
			: base( serial )
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
