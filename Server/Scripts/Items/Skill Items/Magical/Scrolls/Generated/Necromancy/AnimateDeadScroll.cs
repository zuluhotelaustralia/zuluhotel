using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class AnimateDeadScroll : SpellScroll
	{
		[Constructable]
		public AnimateDeadScroll() : this( 1 )
		{
		    Name = "Animate Dead";
		    Hue = 0x66D;
		}

		[Constructable]
		public AnimateDeadScroll( int amount ) : base( 105, 0x2260, amount )
		{
		    Name = "Animate Dead";
		    Hue = 0x66D;

		}

		public AnimateDeadScroll( Serial serial ) : base( serial )
		{
		    Name = "Animate Dead";
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
