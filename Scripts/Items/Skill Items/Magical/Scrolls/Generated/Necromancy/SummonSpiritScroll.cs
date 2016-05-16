using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SummonSpiritScrollScroll : SpellScroll
	{
		[Constructable]
		public SummonSpiritScroll() : this( 1 )
		{
		}

		[Constructable]
		public SummonSpiritScroll( int amount ) : base( 109, 0x2260, amount )
		{
		}

		public SummonSpiritScroll( Serial serial ) : base( serial )
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