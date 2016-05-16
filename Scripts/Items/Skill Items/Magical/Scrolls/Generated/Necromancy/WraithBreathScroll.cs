using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class WraithBreathScrollScroll : SpellScroll
	{
		[Constructable]
		public WraithBreathScroll() : this( 1 )
		{
		}

		[Constructable]
		public WraithBreathScroll( int amount ) : base( 107, 0x2260, amount )
		{
		}

		public WraithBreathScroll( Serial serial ) : base( serial )
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