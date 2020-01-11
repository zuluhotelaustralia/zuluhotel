using System;
using Server;

namespace Server.Items
{
    public class PaganVendorSuit : BaseSuit
    {
	[Constructable]
	public PaganVendorSuit() : base( AccessLevel.Player, 2749, 0x204E )
	{
	    this.Name = "a tattered robe";
	}

	public PaganVendorSuit( Serial serial ) : base( serial )
	{
	    this.Name = "a tattered robe";
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
