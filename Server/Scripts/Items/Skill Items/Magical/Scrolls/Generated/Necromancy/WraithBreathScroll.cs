using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class WraithBreathScroll : SpellScroll
	{
		[Constructable]
		public WraithBreathScroll() : this( 1 )
		{
		    Name = "Wraith Breath";
		    Hue = 0x66D;
		}

		[Constructable]
		public WraithBreathScroll( int amount ) : base( 107, 0x2260, amount )
		{
		    Name = "Wraith Breath";
		    Hue = 0x66D;
		}

		public WraithBreathScroll( Serial serial ) : base( serial )
		{
		    Name = "Wraith Breath";
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
