using Server.Engines.Craft;

namespace Server.Items
{
    [FlipableAttribute( 0xfbb, 0xfbc )]
	public class Tongs : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBlacksmithy.CraftSystem; } }


		[Constructible]
public Tongs() : base( 0xFBB )
		{
			Weight = 2.0;
		}


		[Constructible]
public Tongs( int uses ) : base( uses, 0xFBB )
		{
			Weight = 2.0;
		}

		[Constructible]
public Tongs( Serial serial ) : base( serial )
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
