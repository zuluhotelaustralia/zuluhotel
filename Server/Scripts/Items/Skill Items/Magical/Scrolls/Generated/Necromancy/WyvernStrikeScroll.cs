using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class WyvernStrikeScroll : SpellScroll
	{
		[Constructable]
		public WyvernStrikeScroll() : this( 1 )
		{
		    Name = "Wyvern Strike";
		    Hue = 0x66D;
		}

		[Constructable]
		public WyvernStrikeScroll( int amount ) : base( 111, 0x2260, amount )
		{
		    Name = "Wyvern Strike";
		    Hue = 0x66D;
		}

		public WyvernStrikeScroll( Serial serial ) : base( serial )
		{
		    Name = "Wyvern Strike";
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
