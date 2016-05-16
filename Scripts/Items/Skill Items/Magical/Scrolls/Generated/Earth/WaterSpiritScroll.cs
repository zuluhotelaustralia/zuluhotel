using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class WaterSpiritScrollScroll : SpellScroll
	{
		[Constructable]
		public WaterSpiritScroll() : this( 1 )
		{
		}

		[Constructable]
		public WaterSpiritScroll( int amount ) : base( 617, 0x2260, amount )
		{
		}

		public WaterSpiritScroll( Serial serial ) : base( serial )
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