using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ShiftingEarthScrollScroll : SpellScroll
	{
		[Constructable]
		public ShiftingEarthScroll() : this( 1 )
		{
		}

		[Constructable]
		public ShiftingEarthScroll( int amount ) : base( 602, 0x2260, amount )
		{
		}

		public ShiftingEarthScroll( Serial serial ) : base( serial )
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