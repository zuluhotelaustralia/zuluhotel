using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class KillScroll : SpellScroll
	{
		[Constructable]
		public KillScroll() : this( 1 )
		{
		    Name = "Kill";
		    Hue = 0x66D;
		}

		[Constructable]
		public KillScroll( int amount ) : base( 112, 0x2260, amount )
		{
		    Name = "Kill";
		    Hue = 0x66D;
		}

		public KillScroll( Serial serial ) : base( serial )
		{
		    Name = "Kill";
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
