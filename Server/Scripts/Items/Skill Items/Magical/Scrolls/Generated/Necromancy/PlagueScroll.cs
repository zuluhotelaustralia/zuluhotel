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
		    Name = "Plague";
		    Hue = 0x66D;
		}

		[Constructable]
		public PlagueScroll( int amount ) : base( 114, 0x2260, amount )
		{
		    Name = "Plague";
		    Hue = 0x66D;
		}

		public PlagueScroll( Serial serial ) : base( serial )
		{
		    Name = "Plague";
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
