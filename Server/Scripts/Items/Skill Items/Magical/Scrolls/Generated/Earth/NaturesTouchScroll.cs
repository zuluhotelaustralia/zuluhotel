using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class NaturesTouchScroll : SpellScroll
	{
		[Constructable]
		public NaturesTouchScroll() : this( 1 )
		{
		    this.Name = "Nature's Touch";
		}

		[Constructable]
		public NaturesTouchScroll( int amount ) : base( 607, 0x2260, amount )
		{
		}

		public NaturesTouchScroll( Serial serial ) : base( serial )
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
