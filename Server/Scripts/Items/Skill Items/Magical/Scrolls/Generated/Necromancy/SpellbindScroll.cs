using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SpellbindScroll : SpellScroll
	{
		[Constructable]
		public SpellbindScroll() : this( 1 )
		{
		}

		[Constructable]
		public SpellbindScroll( int amount ) : base( 115, 0x2260, amount )
		{
		}

		public SpellbindScroll( Serial serial ) : base( serial )
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