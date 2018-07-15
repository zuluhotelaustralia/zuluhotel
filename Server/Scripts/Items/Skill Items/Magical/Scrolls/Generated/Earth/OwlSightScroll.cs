using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class OwlSightScroll : SpellScroll
	{
		[Constructable]
		public OwlSightScroll() : this( 1 )
		{
		    this.Name = "Owl Sight";
		}

		[Constructable]
		public OwlSightScroll( int amount ) : base( 601, 0x2260, amount )
		{
		}

		public OwlSightScroll( Serial serial ) : base( serial )
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
