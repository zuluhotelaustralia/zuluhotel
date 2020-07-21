namespace Server.Items
{
    public class SpecialDyeTub : DyeTub
	{
		public override CustomHuePicker CustomHuePicker{ get{ return CustomHuePicker.SpecialDyeTub; } }
		public override int LabelNumber{ get{ return 1041285; } } // Special Dye Tub


		[Constructible]
public SpecialDyeTub()
		{
			LootType = LootType.Blessed;
		}

		[Constructible]
public SpecialDyeTub( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
