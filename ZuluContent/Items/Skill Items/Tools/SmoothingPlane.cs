using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable( 0x1032, 0x1033 )]
	public class SmoothingPlane : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCarpentry.CraftSystem; } }


		[Constructible]
public SmoothingPlane() : base( 0x1032 )
		{
			Weight = 1.0;
		}


		[Constructible]
public SmoothingPlane( int uses ) : base( uses, 0x1032 )
		{
			Weight = 1.0;
		}

		[Constructible]
public SmoothingPlane( Serial serial ) : base( serial )
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
