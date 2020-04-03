using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class WraithFormScroll : SpellScroll
    {
	[Constructable]
	public WraithFormScroll() : this( 1 )
	{
	    Name = "Wraith Form";
	    Hue = 0x66D;
	}

	[Constructable]
	public WraithFormScroll( int amount ) : base( 110, 0x2260, amount )
	{
	    Name = "Wraith Form";
	    Hue = 0x66D;
	}

	public WraithFormScroll( Serial serial ) : base( serial )
	{
	    Name = "Wraith Form";
	    Hue = 0x66D;
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
