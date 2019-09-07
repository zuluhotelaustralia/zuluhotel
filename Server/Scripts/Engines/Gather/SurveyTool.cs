using System;
using Server;
using Server.Engines.Gather;

namespace Server.Items
{
    public class SurveyTool : Item
    {
	[Constructable]
	public SurveyTool() : this( 50 )
	{
	}

	[Constructable]
	public SurveyTool( int uses ) : base( uses, 0xF39 )
	{
	    Weight = 5.0;
	}

	public SurveyTool( Serial serial ) : base( serial )
	{
	}

	public override void OnDoubleClick( Mobile from ){
	}

	public void MunchMunch( Mobile from ){
	    from.PlaySound( Utility.Random( 0x3A, 3 ) );
	    
	    if ( from.Body.IsHuman && !from.Mounted ){
		from.Animate( 34, 5, 1, true, false, 0 );
	    }
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
