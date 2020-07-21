using Server.Engines.Craft;

namespace Server.Items
{
    public class SewingKit : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefTailoring.CraftSystem; } }


		[Constructible]
public SewingKit() : base( 0xF9D )
		{
			Weight = 2.0;
		}


		[Constructible]
public SewingKit( int uses ) : base( uses, 0xF9D )
		{
			Weight = 2.0;
		}

		[Constructible]
public SewingKit( Serial serial ) : base( serial )
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
