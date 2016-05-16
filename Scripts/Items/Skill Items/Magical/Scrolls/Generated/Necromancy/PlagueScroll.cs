using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PlagueScroll : SpellScroll
	{
		[Constructable]
		public PlagueScroll() : this( 1 )
		{
		}

		[Constructable]
		public PlagueScroll( int amount ) : base( 114, 0x2260, amount )
		{
		}

		public PlagueScroll( Serial serial ) : base( serial )
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