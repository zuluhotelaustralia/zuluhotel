using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class StormSpiritScroll : SpellScroll
	{
            public override int LabelNumber { get { return 1031615; } }
            
            
		[Constructable]
		public StormSpiritScroll() : this( 1 )
		{
		}

		[Constructable]
		public StormSpiritScroll( int amount ) : base( 614, 0x2260, amount )
		{
		}

		public StormSpiritScroll( Serial serial ) : base( serial )
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
