using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SorcerorsBaneScroll : SpellScroll
	{
		[Constructable]
		public SorcerorsBaneScroll() : this( 1 )
		{
		}

		[Constructable]
		public SorcerorsBaneScroll( int amount ) : base( 100, 0x2260, amount )
		{
		}

		public SorcerorsBaneScroll( Serial serial ) : base( serial )
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
