namespace Server.Items
{
    public class FurnitureDyeTub : DyeTub
	{
		public override bool AllowDyables{ get{ return false; } }
		public override bool AllowFurniture{ get{ return true; } }
		public override int TargetMessage{ get{ return 501019; } } // Select the furniture to dye.
		public override int FailMessage{ get{ return 501021; } } // That is not a piece of furniture.
		public override int LabelNumber{ get{ return 1041246; } } // Furniture Dye Tub


		[Constructible]
public FurnitureDyeTub()
		{
			LootType = LootType.Blessed;
		}

		[Constructible]
public FurnitureDyeTub( Serial serial ) : base( serial )
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
