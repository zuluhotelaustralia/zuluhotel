using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CallLightningScroll : SpellScroll
	{
		[Constructable]
		public CallLightningScroll() : this( 1 )
		{
		    this.Name = "Call Lightning";
		}

		[Constructable]
		public CallLightningScroll( int amount ) : base( 604, 0x2260, amount )
		{
		}

		public CallLightningScroll( Serial serial ) : base( serial )
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
