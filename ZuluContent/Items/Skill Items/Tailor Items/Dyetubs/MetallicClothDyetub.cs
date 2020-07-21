namespace Server.Items
{
    public class MetallicClothDyetub : DyeTub
	{
		public override int LabelNumber { get { return 1152920; } } // Metallic Cloth ...

		public override bool MetallicHues { get { return true; } }


		[Constructible]
public MetallicClothDyetub()
		{
			LootType = LootType.Blessed;
		}

		[Constructible]
public MetallicClothDyetub( Serial serial )
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
