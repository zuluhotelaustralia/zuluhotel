using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable( 0x1030, 0x1031 )]
	public class JointingPlane : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCarpentry.CraftSystem; } }


		[Constructible]
public JointingPlane() : base( 0x1030 )
		{
			Weight = 2.0;
		}


		[Constructible]
public JointingPlane( int uses ) : base( uses, 0x1030 )
		{
			Weight = 2.0;
		}

		[Constructible]
public JointingPlane( Serial serial ) : base( serial )
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

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}
