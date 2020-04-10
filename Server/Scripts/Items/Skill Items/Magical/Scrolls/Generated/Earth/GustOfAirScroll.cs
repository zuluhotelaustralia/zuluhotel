using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class GustOfAirScroll : SpellScroll
	{
            public override int LabelNumber { get { return 1031609; } }
            
		[Constructable]
		public GustOfAirScroll() : this( 1 )
		{
		}

		[Constructable]
		public GustOfAirScroll( int amount ) : base( 608, 0x2260, amount )
		{
		}

		public GustOfAirScroll( Serial serial ) : base( serial )
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
