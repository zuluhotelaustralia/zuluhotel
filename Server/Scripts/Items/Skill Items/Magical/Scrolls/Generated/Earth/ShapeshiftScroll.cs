using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ShapeshiftScroll : SpellScroll
	{
		[Constructable]
		public ShapeshiftScroll() : this( 1 )
		{
		    this.Name = "Shapeshift";
		}

		[Constructable]
		public ShapeshiftScroll( int amount ) : base( 612, 0x2260, amount )
		{
		}

		public ShapeshiftScroll( Serial serial ) : base( serial )
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
