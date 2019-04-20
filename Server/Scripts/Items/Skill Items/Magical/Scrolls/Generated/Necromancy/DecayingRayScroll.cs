using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class DecayingRayScroll : SpellScroll
    {
	[Constructable]
	public DecayingRayScroll() : this( 1 )
	{
	    this.Name = "Decaying Ray";
	}

	[Constructable]
	public DecayingRayScroll( int amount ) : base( 102, 0x2260, amount )
	{
	}

	public DecayingRayScroll( Serial serial ) : base( serial )
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
