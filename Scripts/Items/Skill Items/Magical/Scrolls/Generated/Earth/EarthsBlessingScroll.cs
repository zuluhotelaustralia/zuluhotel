using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EarthsBlessingScrollScroll : SpellScroll
	{
		[Constructable]
		public EarthsBlessingScroll() : this( 1 )
		{
		}

		[Constructable]
		public EarthsBlessingScroll( int amount ) : base( 605, 0x2260, amount )
		{
		}

		public EarthsBlessingScroll( Serial serial ) : base( serial )
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