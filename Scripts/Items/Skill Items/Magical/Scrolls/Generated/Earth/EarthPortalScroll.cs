using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EarthPortalScroll : SpellScroll
	{
		[Constructable]
		public EarthPortalScroll() : this( 1 )
		{
		}

		[Constructable]
		public EarthPortalScroll( int amount ) : base( 606, 0x2260, amount )
		{
		}

		public EarthPortalScroll( Serial serial ) : base( serial )
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