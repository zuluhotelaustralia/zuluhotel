using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class AntidoteScroll : SpellScroll
	{
            public override int LabelNumber { get { return 1031601; } }
            
		[Constructable]
		public AntidoteScroll() : this( 1 )
		{
		}

		[Constructable]
		public AntidoteScroll( int amount ) : base( 600, 0x2260, amount )
		{
		}

		public AntidoteScroll( Serial serial ) : base( serial )
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
