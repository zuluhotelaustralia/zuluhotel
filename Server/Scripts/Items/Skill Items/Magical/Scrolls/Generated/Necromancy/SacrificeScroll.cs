using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SacrificeScroll : SpellScroll
	{
		[Constructable]
		public SacrificeScroll() : this( 1 )
		{
		}

		[Constructable]
		public SacrificeScroll( int amount ) : base( 106, 0x2260, amount )
		{
		}

		public SacrificeScroll( Serial serial ) : base( serial )
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