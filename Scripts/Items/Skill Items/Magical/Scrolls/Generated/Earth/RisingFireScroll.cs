using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RisingFireScrollScroll : SpellScroll
	{
		[Constructable]
		public RisingFireScroll() : this( 1 )
		{
		}

		[Constructable]
		public RisingFireScroll( int amount ) : base( 610, 0x2260, amount )
		{
		}

		public RisingFireScroll( Serial serial ) : base( serial )
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