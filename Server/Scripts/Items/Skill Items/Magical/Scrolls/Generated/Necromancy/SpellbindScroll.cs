using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SpellbindScroll : SpellScroll
	{
            public override int LabelNumber { get { return 1060524; } }
            
		[Constructable]
		public SpellbindScroll() : this( 1 )
		{
		}

		[Constructable]
		public SpellbindScroll( int amount ) : base( 115, 0x2260, amount )
		{
		    Hue = 0x66D;
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
