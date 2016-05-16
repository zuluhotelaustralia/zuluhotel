using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class LicheFormScrollScroll : SpellScroll
	{
		[Constructable]
		public LicheFormScroll() : this( 1 )
		{
		}

		[Constructable]
		public LicheFormScroll( int amount ) : base( 113, 0x2260, amount )
		{
		}

		public LicheFormScroll( Serial serial ) : base( serial )
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