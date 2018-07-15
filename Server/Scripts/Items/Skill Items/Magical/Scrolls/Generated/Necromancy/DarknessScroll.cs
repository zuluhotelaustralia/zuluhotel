using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DarknessScroll : SpellScroll
	{
		[Constructable]
		public DarknessScroll() : this( 1 )
		{
		    this.Name = "Darkness";
		}

		[Constructable]
		public DarknessScroll( int amount ) : base( 101, 0x2260, amount )
		{
		}

		public DarknessScroll( Serial serial ) : base( serial )
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
