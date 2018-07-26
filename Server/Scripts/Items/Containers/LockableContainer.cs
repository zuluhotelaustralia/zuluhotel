using System;
using Server.Network;
using Server.Items;
using Server.Engines.Craft;

namespace Server.Items
{
    public abstract class LockableContainer : TrapableContainer, ILockable, ILockpickable, ICraftable, IShipwreckedItem
    {
	private bool m_Locked;
	private int m_LockLevel, m_MaxLockLevel, m_RequiredSkill;
	private uint m_KeyValue;
	private Mobile m_Picker;
	private bool m_TrapOnLockpick;

	private CraftResource m_Resource;

	[CommandProperty( AccessLevel.GameMaster )]
	public CraftResource Resource {
	    get { return m_Resource; }
	    set {
		if( m_Resource != value ){
		    m_Resource = value;
		    Hue = CraftResources.GetHue(m_Resource);
		}
	    }
	}
	
	[CommandProperty( AccessLevel.GameMaster )]
	public Mobile Picker
	{
	    get
	    {
		return m_Picker;
	    }
	    set
	    {
		m_Picker = value;
	    }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public int MaxLockLevel
	{
	    get
	    {
		return m_MaxLockLevel;
	    }
	    set
	    {
		m_MaxLockLevel = value;
	    }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public int LockLevel
	{
	    get
	    {
		return m_LockLevel;
	    }
	    set
	    {
		m_LockLevel = value;
	    }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public int RequiredSkill
	{
	    get
	    {
		return m_RequiredSkill;
	    }
	    set
	    {
		m_RequiredSkill = value;
	    }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public virtual bool Locked
	{
	    get
	    {
		return m_Locked;
	    }
	    set
	    {
		m_Locked = value;

		if ( m_Locked )
		    m_Picker = null;

		InvalidateProperties();
	    }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public uint KeyValue
	{
	    get
	    {
		return m_KeyValue;
	    }
	    set
	    {
		m_KeyValue = value;
	    }
	}

	public override bool TrapOnOpen
	{
	    get
	    {
		return !m_TrapOnLockpick;
	    }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool TrapOnLockpick
	{
	    get
	    {
		return m_TrapOnLockpick;
	    }
	    set
	    {
		m_TrapOnLockpick = value;
	    }
	}

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );

	    writer.Write( (int) 6 ); // version

	    writer.Write( m_IsShipwreckedItem );

	    writer.Write( (bool) m_TrapOnLockpick );

	    writer.Write( (int) m_RequiredSkill );

	    writer.Write( (int) m_MaxLockLevel );

	    writer.Write( m_KeyValue );
	    writer.Write( (int) m_LockLevel );
	    writer.Write( (bool) m_Locked );
	}

	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );

	    int version = reader.ReadInt();

	    switch ( version )
	    {
		case 6:
		    {
			m_IsShipwreckedItem = reader.ReadBool();

			goto case 5;
		    }
		case 5:
		    {
			m_TrapOnLockpick = reader.ReadBool();

			goto case 4;
		    }
		case 4:
		    {
			m_RequiredSkill = reader.ReadInt();

			goto case 3;
		    }
		case 3:
		    {
			m_MaxLockLevel = reader.ReadInt();

			goto case 2;
		    }
		case 2:
		    {
			m_KeyValue = reader.ReadUInt();

			goto case 1;
		    }
		case 1:
		    {
			m_LockLevel = reader.ReadInt();

			goto case 0;
		    }
		case 0:
		    {
			if ( version < 3 )
			    m_MaxLockLevel = 100;

			if ( version < 4 )
			{
			    if ( (m_MaxLockLevel - m_LockLevel) == 40 )
			    {
				m_RequiredSkill = m_LockLevel + 6;
				m_LockLevel = m_RequiredSkill - 10;
				m_MaxLockLevel = m_RequiredSkill + 39;
			    }
			    else
			    {
				m_RequiredSkill = m_LockLevel;
			    }
			}

			m_Locked = reader.ReadBool();

			break;
		    }
	    }
	}

	public LockableContainer( int itemID ) : base( itemID )
	{
	    m_MaxLockLevel = 100;
	}

	public LockableContainer( Serial serial ) : base( serial )
	{
	}

	public override bool CheckContentDisplay( Mobile from )
	{
	    return !m_Locked && base.CheckContentDisplay( from );
	}

	public override bool TryDropItem( Mobile from, Item dropped, bool sendFullMessage )
	{
	    if ( from.AccessLevel < AccessLevel.GameMaster && m_Locked )
	    {
		from.SendLocalizedMessage( 501747 ); // It appears to be locked.
		return false;
	    }

	    return base.TryDropItem( from, dropped, sendFullMessage );
	}

	public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
	{
	    if ( from.AccessLevel < AccessLevel.GameMaster && m_Locked )
	    {
		from.SendLocalizedMessage( 501747 ); // It appears to be locked.
		return false;
	    }

	    return base.OnDragDropInto( from, item, p );
	}

	public override bool CheckLift( Mobile from, Item item, ref LRReason reject )
	{
	    if ( !base.CheckLift( from, item, ref reject ) )
		return false;

	    if ( item != this && from.AccessLevel < AccessLevel.GameMaster && m_Locked )
		return false;

	    return true;
	}

	public override bool CheckItemUse( Mobile from, Item item )
	{
	    if ( !base.CheckItemUse( from, item ) )
		return false;

	    if ( item != this && from.AccessLevel < AccessLevel.GameMaster && m_Locked )
	    {
		from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
		return false;
	    }

	    return true;
	}

	public override bool DisplaysContent{ get{ return !m_Locked; } }

	public virtual bool CheckLocked( Mobile from )
	{
	    bool inaccessible = false;

	    if ( m_Locked )
	    {
		int number;

		if ( from.AccessLevel >= AccessLevel.GameMaster )
		{
		    number = 502502; // That is locked, but you open it with your godly powers.
		}
		else
		{
		    number = 501747; // It appears to be locked.
		    inaccessible = true;
		}

		from.Send( new MessageLocalized( Serial, ItemID, MessageType.Regular, 0x3B2, 3, number, "", "" ) );
	    }

	    return inaccessible;
	}

	public override void OnTelekinesis( Mobile from )
	{
	    if ( CheckLocked( from ) )
	    {
		Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5022 );
		Effects.PlaySound( Location, Map, 0x1F5 );
		return;
	    }

	    base.OnTelekinesis( from );
	}

	public override void OnDoubleClickSecureTrade( Mobile from )
	{
	    if ( CheckLocked( from ) )
		return;

	    base.OnDoubleClickSecureTrade( from );
	}

	public override void Open( Mobile from )
	{
	    if ( CheckLocked( from ) )
		return;

	    base.Open( from );
	}

	public override void OnSnoop( Mobile from )
	{
	    if ( CheckLocked( from ) )
		return;

	    base.OnSnoop( from );
	}

	public virtual void LockPick( Mobile from )
	{
	    Locked = false;
	    Picker = from;

	    if ( this.TrapOnLockpick && ExecuteTrap( from ) )
	    {
		this.TrapOnLockpick = false;
	    }
	}

	private string GetNameString()
	{
	    string name = this.Name;

	    if( name == null )
		name = String.Format("#{0}", LabelNumber );

	    return name;
	}
	
	public override void AddNameProperty( ObjectPropertyList list ){
	    int oreType; 

	    switch ( m_Resource ){
		case CraftResource.Gold: oreType = 1160000; break;
		case CraftResource.Spike: oreType = 1160001; break;
		case CraftResource.Fruity: oreType = 1160002; break;
		case CraftResource.Bronze: oreType = 1160003; break;
		case CraftResource.IceRock: oreType = 1160004; break;
		case CraftResource.BlackDwarf: oreType = 1160005; break;
		case CraftResource.DullCopper: oreType = 1160006; break;
		case CraftResource.Platinum: oreType = 1160007; break;
		case CraftResource.SilverRock: oreType = 1160008; break;
		case CraftResource.DarkPagan: oreType = 1160009; break;
		case CraftResource.Copper: oreType = 1160010; break;
		case CraftResource.Mystic: oreType = 1160011; break;
		case CraftResource.Spectral: oreType = 1160012; break;
		case CraftResource.OldBritain: oreType = 1160013; break;
		case CraftResource.Onyx: oreType = 1160014; break;
		case CraftResource.RedElven: oreType = 1160015; break;
		case CraftResource.Undead: oreType = 1160016; break;
		case CraftResource.Pyrite: oreType = 1160017; break;
		case CraftResource.Virginity: oreType = 1160018; break;
		case CraftResource.Malachite: oreType = 1160019; break;
		case CraftResource.Lavarock: oreType = 1160020; break;
		case CraftResource.Azurite: oreType = 1160021; break;
		case CraftResource.Dripstone: oreType = 1160022; break;
		case CraftResource.Executor: oreType = 1160023; break;
		case CraftResource.Peachblue: oreType = 1160024; break;
		case CraftResource.Destruction: oreType = 1160025; break;
		case CraftResource.Anra: oreType = 1160026; break;
		case CraftResource.Crystal: oreType = 1160027; break;
		case CraftResource.Doom: oreType = 1160028; break;
		case CraftResource.Goddess: oreType = 1160029; break;
		case CraftResource.NewZulu: oreType = 1160030; break;
		case CraftResource.DarkSableRuby: oreType = 1160031; break;
		case CraftResource.EbonTwilightSapphire: oreType = 1160032; break;
		case CraftResource.RadiantNimbusDiamond: oreType = 1160033; break;
		default: oreType = 0; break;
	    }

	    
	    if ( oreType != 0 )
		list.Add( 1053099, "#{0}\t{1}", oreType, GetNameString() ); // ~1_oretype~ ~2_armortype~
	    else if ( Name == null )
		list.Add( LabelNumber );
	    else
		list.Add( Name );
	}

	public override void AddNameProperties( ObjectPropertyList list )
	{
	    base.AddNameProperties( list );

	    if ( m_IsShipwreckedItem )
		list.Add( 1041645 ); // recovered from a shipwreck
	}

	public override void OnSingleClick( Mobile from )
	{
	    base.OnSingleClick( from );

	    if ( m_IsShipwreckedItem )
		LabelTo( from, 1041645 );	//recovered from a shipwreck
	}

	

	#region ICraftable Members

	public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
	{
	    Type resourceType = typeRes;

	    if( resourceType == null ){
		resourceType = craftItem.Resources.GetAt(0).ItemType;
	    }
	    
	    if ( from.CheckSkill( SkillName.Tinkering, -5.0, 15.0 ) )
	    {
		from.SendLocalizedMessage( 500636 ); // Your tinker skill was sufficient to make the item lockable.

		Key key = new Key( KeyType.Copper, Key.RandomValue() );

		KeyValue = key.KeyValue;
		DropItem( key );

		double tinkering = from.Skills[SkillName.Tinkering].Value;
		int level = (int)(tinkering * 0.8);

		RequiredSkill = level - 4;
		LockLevel = level - 14;
		MaxLockLevel = level + 35;

		if ( LockLevel == 0 )
		    LockLevel = -1;
		else if ( LockLevel > 95 )
		    LockLevel = 95;

		if ( RequiredSkill > 95 )
		    RequiredSkill = 95;

		if ( MaxLockLevel > 95 )
		    MaxLockLevel = 95;
	    }
	    else
	    {
		from.SendLocalizedMessage( 500637 ); // Your tinker skill was insufficient to make the item lockable.
	    }

	    return 1;
	}

	#endregion

	#region IShipwreckedItem Members

	private bool m_IsShipwreckedItem;

	[CommandProperty( AccessLevel.GameMaster )]
	public bool IsShipwreckedItem
	{
	    get { return m_IsShipwreckedItem; }
	    set { m_IsShipwreckedItem = value; }
	}
	#endregion

    }
}
