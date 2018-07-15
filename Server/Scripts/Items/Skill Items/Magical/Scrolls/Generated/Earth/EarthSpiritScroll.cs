using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EarthSpiritScroll : SpellScroll
	{
		[Constructable]
		public EarthSpiritScroll() : this( 1 )
		{
		    this.Name = "Earth Spirit";
		}

		[Constructable]
		public EarthSpiritScroll( int amount ) : base( 614, 0x2260, amount )
		{
		}

		public EarthSpiritScroll( Serial serial ) : base( serial )
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
