using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class IceStrikeScroll : SpellScroll
	{
		[Constructable]
		public IceStrikeScroll() : this( 1 )
		{
		    this.Name = "Ice Strike";
		}

		[Constructable]
		public IceStrikeScroll( int amount ) : base( 611, 0x2260, amount )
		{
		}

		public IceStrikeScroll( Serial serial ) : base( serial )
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
