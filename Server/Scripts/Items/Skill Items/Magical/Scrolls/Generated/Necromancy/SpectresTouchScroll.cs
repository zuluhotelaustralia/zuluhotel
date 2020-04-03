using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SpectresTouchScroll : SpellScroll
	{
		[Constructable]
		public SpectresTouchScroll() : this( 1 )
		{
		    Name = "Spectre's Touch";
		    Hue = 0x66D;
		}

		[Constructable]
		public SpectresTouchScroll( int amount ) : base( 103, 0x2260, amount )
		{
		    Name = "Spectre's Touch";
		    Hue = 0x66D;
		}

		public SpectresTouchScroll( Serial serial ) : base( serial )
		{
		    Name = "Spectre's Touch";
		    Hue = 0x66D;
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
