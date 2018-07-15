using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class AbyssalFlameScroll : SpellScroll
	{
		[Constructable]
		public AbyssalFlameScroll() : this( 1 )
		{
		    this.Name = "Abyssal Flame";
		}

		[Constructable]
		public AbyssalFlameScroll( int amount ) : base( 104, 0x2260, amount )
		{
		}

		public AbyssalFlameScroll( Serial serial ) : base( serial )
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
