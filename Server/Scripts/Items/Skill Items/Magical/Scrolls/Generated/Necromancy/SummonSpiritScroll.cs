using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SummonSpiritScroll : SpellScroll
	{
		[Constructable]
		public SummonSpiritScroll() : this( 1 )
		{
		    Name = "Summon Spirit";
		    Hue = 0x66D;
		}

		[Constructable]
		public SummonSpiritScroll( int amount ) : base( 109, 0x2260, amount )
		{
		    Name = "Summon Spirit";
		    Hue = 0x66D;
		}

		public SummonSpiritScroll( Serial serial ) : base( serial )
		{
		    Name = "Summon Spirit";
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
