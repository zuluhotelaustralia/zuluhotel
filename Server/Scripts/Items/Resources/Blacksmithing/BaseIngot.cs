using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public abstract class BaseIngot : Item, ICommodity
    {
	private CraftResource m_Resource;

	[CommandProperty( AccessLevel.GameMaster )]
	public CraftResource Resource
	{
	    get{ return m_Resource; }
	    set{ m_Resource = value; InvalidateProperties(); }
	}

	public override double DefaultWeight
	{
	    get { return 0.1; }
	}

	int ICommodity.DescriptionNumber { get { return LabelNumber; } }
	bool ICommodity.IsDeedable { get { return true; } }

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );

	    writer.Write( (int) 1 ); // version

	    writer.Write( (int) m_Resource );
	}

	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );

	    int version = reader.ReadInt();

	    switch ( version )
	    {
		case 1:
		    {
			m_Resource = (CraftResource)reader.ReadInt();
			break;
		    }
		case 0:
		    {
			throw new System.Exception("Unsupported item version for ingot.");
		    }
	    }
	}

	public BaseIngot( CraftResource resource ) : this( resource, 1 )
	{
	}

	public BaseIngot( CraftResource resource, int amount ) : base( 0x1BF2 )
	{
	    Stackable = true;
	    Amount = amount;
	    //Hue = CraftResources.GetHue( resource );

	    m_Resource = resource;
	}

	public BaseIngot( Serial serial ) : base( serial )
	{
	}

	public override void AddNameProperty( ObjectPropertyList list )
	{
            int resourceCliloc = CraftResources.GetLocalizationNumber(m_Resource);
            
            if ( Amount > 1 )
                list.Add( 1160100, "{0}\t#{1}\t#{2}", Amount, resourceCliloc, 1160103 );
            else
                list.Add( 1160101, "#{0}\t#{1}", resourceCliloc, 1160102 );
	}

	public override void GetProperties( ObjectPropertyList list )
	{
	    base.GetProperties( list );

	    if ( !CraftResources.IsStandard( m_Resource ) )
	    {
		int num = CraftResources.GetLocalizationNumber( m_Resource );

		if ( num > 0 )
		    list.Add( num );
		else
		    list.Add( CraftResources.GetName( m_Resource ) );
	    }
	}

	public override int LabelNumber
	{
	    get
	    {
		if ( m_Resource >= CraftResource.Gold && m_Resource <= CraftResource.RadiantNimbusDiamond )
		    return 1161034 + (int)(m_Resource - CraftResource.Gold);

		return Amount > 1 ? 1160103 : 1160102;
	    }
	}
    }
}
