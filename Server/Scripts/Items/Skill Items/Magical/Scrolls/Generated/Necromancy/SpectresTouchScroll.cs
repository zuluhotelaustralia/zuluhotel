using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SpectresTouchScroll : SpellScroll
	{
            public override int LabelNumber { get { return 1060512; } }
            
		[Constructable]
		public SpectresTouchScroll() : this( 1 )
		{
		}

		[Constructable]
		public SpectresTouchScroll( int amount ) : base( 103, 0x2260, amount )
		{
		    Hue = 0x66D;
		}

		public SpectresTouchScroll( Serial serial ) : base( serial )
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
