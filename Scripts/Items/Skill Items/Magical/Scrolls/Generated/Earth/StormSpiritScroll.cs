using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class StormSpiritScrollScroll : SpellScroll
	{
		[Constructable]
		public StormSpiritScroll() : this( 1 )
		{
		}

		[Constructable]
		public StormSpiritScroll( int amount ) : base( 616, 0x2260, amount )
		{
		}

		public StormSpiritScroll( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}


	}
}