using System;
using Server;
using Server.Network;
using Server.Engines.Craft;

namespace Server.Items
{
    public enum GemType
    {
	None,
	StarSapphire,
	Emerald,
	Sapphire,
	Ruby,
	Citrine,
	Amethyst,
	Tourmaline,
	Amber,
	Diamond
    }

    public abstract class BaseJewel : Item, ICraftable
    {
	private int m_MaxHitPoints;
	private int m_HitPoints;

	private AosAttributes m_AosAttributes;
	private AosElementAttributes m_AosResistances;
	private AosSkillBonuses m_AosSkillBonuses;
	private ZuluSkillMods m_ZuluSkillMods;
	private int m_VirtualArmorMod;
	private CraftResource m_Resource;
	private GemType m_GemType;
	private int m_StrBonus = 0, m_DexBonus = 0, m_IntBonus = 0;
	private bool m_Identified = false;

	[CommandProperty( AccessLevel.GameMaster )]
	public int VirtualArmorMod {
	    get { return m_VirtualArmorMod; }
	    set { m_VirtualArmorMod = value; }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool Identified {
	    get { return m_Identified; }
	    set { m_Identified = value; }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public int StrBonus {
	    get { return m_StrBonus; }
	    set { m_StrBonus = value; }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public int DexBonus {
	    get { return m_DexBonus; }
	    set { m_DexBonus = value; }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public int IntBonus {
	    get { return m_IntBonus; }
	    set { m_IntBonus = value; }
	}
	[CommandProperty( AccessLevel.GameMaster )]
	public int MaxHitPoints
	{
	    get{ return m_MaxHitPoints; }
	    set{ m_MaxHitPoints = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public int HitPoints
	{
	    get 
	    {
		return m_HitPoints;
	    }
	    set 
	    {
		if ( value != m_HitPoints && MaxHitPoints > 0 )
		{
		    m_HitPoints = value;

		    if ( m_HitPoints < 0 )
			Delete();
		    else if ( m_HitPoints > MaxHitPoints )
			m_HitPoints = MaxHitPoints;

		    InvalidateProperties();
		}
	    }
	}

	[CommandProperty( AccessLevel.Player )]
	public AosAttributes Attributes
	{
	    get{ return m_AosAttributes; }
	    set{}
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public AosElementAttributes Resistances
	{
	    get{ return m_AosResistances; }
	    set{}
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public ZuluSkillMods ZuluSkillMods {
	    get { return m_ZuluSkillMods; }
	}
	
	[CommandProperty( AccessLevel.GameMaster )]
	public AosSkillBonuses SkillBonuses
	{
	    get{ return m_AosSkillBonuses; }
	    set{}
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public CraftResource Resource
	{
	    get{ return m_Resource; }
	    set{ m_Resource = value; Hue = CraftResources.GetHue( m_Resource ); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public GemType GemType
	{
	    get{ return m_GemType; }
	    set{ m_GemType = value; InvalidateProperties(); }
	}

	public override int PhysicalResistance{ get{ return m_AosResistances.Physical; } }
	public override int FireResistance{ get{ return m_AosResistances.Fire; } }
	public override int ColdResistance{ get{ return m_AosResistances.Cold; } }
	public override int PoisonResistance{ get{ return m_AosResistances.Poison; } }
	public override int EnergyResistance{ get{ return m_AosResistances.Energy; } }
	public virtual int BaseGemTypeNumber{ get{ return 0; } }

	public virtual int InitMinHits{ get{ return 0; } }
	public virtual int InitMaxHits{ get{ return 0; } }

	public override int LabelNumber
	{
	    get
	    {
		if ( m_GemType == GemType.None )
		    return base.LabelNumber;

		return BaseGemTypeNumber + (int)m_GemType - 1;
	    }
	}

	public override void OnAfterDuped( Item newItem )
	{
	    BaseJewel jewel = newItem as BaseJewel;

	    if ( jewel == null )
		return;

	    jewel.m_AosAttributes = new AosAttributes( newItem, m_AosAttributes );
	    jewel.m_AosResistances = new AosElementAttributes( newItem, m_AosResistances );
	    jewel.m_AosSkillBonuses = new AosSkillBonuses( newItem, m_AosSkillBonuses );
	    jewel.m_ZuluSkillMods = new ZuluSkillMods( newItem, m_ZuluSkillMods );
	}

	public virtual int ArtifactRarity{ get{ return 0; } }

	public BaseJewel( int itemID, Layer layer ) : base( itemID )
	{
	    m_AosAttributes = new AosAttributes( this );
	    m_AosResistances = new AosElementAttributes( this );
	    m_AosSkillBonuses = new AosSkillBonuses( this );
	    m_ZuluSkillMods = new ZuluSkillMods( this );
	    m_Resource = CraftResource.Iron;
	    m_GemType = GemType.None;
	    m_VirtualArmorMod = 0;
	    Layer = layer;

	    m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax( InitMinHits, InitMaxHits );
	}

	public override void OnAdded(IEntity parent)
	{
	    if ( parent is Mobile )
	    {
		Mobile from = (Mobile)parent;
		
		//m_AosSkillBonuses.AddTo( from );
		m_ZuluSkillMods.AddTo( from );
		from.VirtualArmorMod += m_VirtualArmorMod;
		int strBonus = m_StrBonus;
		int dexBonus = m_DexBonus;
		int intBonus = m_IntBonus;

		if ( strBonus != 0 || dexBonus != 0 || intBonus != 0 )
		{
		    string modName = this.Serial.ToString();

		    if ( strBonus != 0 )
			from.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

		    if ( dexBonus != 0 )
			from.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

		    if ( intBonus != 0 )
			from.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
		}

		from.CheckStatTimers();
	    }
	}

	public override void OnRemoved(IEntity parent)
	{
	    if ( parent is Mobile )
	    {
		Mobile from = (Mobile)parent;

		//m_AosSkillBonuses.Remove();
		m_ZuluSkillMods.Remove( (Mobile)parent );
		from.VirtualArmorMod -= m_VirtualArmorMod;
		string modName = this.Serial.ToString();

		from.RemoveStatMod( modName + "Str" );
		from.RemoveStatMod( modName + "Dex" );
		from.RemoveStatMod( modName + "Int" );

		from.CheckStatTimers();
	    }
	}

	public override void OnSingleClick(Mobile from){
	    if( this.Name == null ){
		String prefix = "";
		if( m_Identified || from.AccessLevel >= AccessLevel.GameMaster ){
		    if ( m_ZuluSkillMods.Mod != null && m_ZuluSkillMods.Mod.Value > 0 ) {
			// TODO: Calculate the "Level" of the skillmod
			SkillMod sk = m_ZuluSkillMods.Mod;
			
			if( sk.Value == 6 ){
			    prefix += "Grandmaster ";
			}
			if( sk.Value == 5 ){
			    prefix += "Master ";
			}
			if( sk.Value == 4 ){
			    prefix += "Adept ";
			}
			if( sk.Value == 3 ){
			    prefix += "Expert ";
			}
			if( sk.Value == 2 ){
			prefix += "Journeyman ";
			}
			if( sk.Value == 1 ){
			    prefix += "Apprentice ";
			}
		    
			prefix += SkillInfo.Table[(int)m_ZuluSkillMods.Mod.Skill].Title + "'s ";
		    }

		    if( this.DexBonus > 0 ){
			if( this.StrBonus == 6 ){
			    prefix += " Escape Artist's ";
			}
			else if( this.StrBonus == 5 ){
			    prefix += " Acrobat's ";
			}
			else if( this.StrBonus == 4 ){
			    prefix += " Tumbler's ";
			}
			else if( this.StrBonus == 3 ){
			    prefix += " Catburglar's ";
			}
			else if( this.StrBonus == 2 ){
			    prefix += " Thief's ";
			}
			else if( this.StrBonus == 1 ){
			    prefix += " Cutpurse's ";
			}
		    }

		    if( this.DexBonus > 0 ){
			if( this.StrBonus == 6 ){
			    prefix += " Oracle's ";
			}
			else if( this.StrBonus == 5 ){
			    prefix += " Archmage's ";
			}
			else if( this.StrBonus == 4 ){
			    prefix += " Magister's ";
			}
			else if( this.StrBonus == 3 ){
			    prefix += " Wizard's ";
			}
			else if( this.StrBonus == 2 ){
			    prefix += " Adept's ";
			}
			else if( this.StrBonus == 1 ){
			    prefix += " Apprentice's ";
			}
		    }

		    if( this.StrBonus > 0 ){
			if( this.StrBonus == 6 ){
			    prefix += " King's ";
			}
			else if( this.StrBonus == 5 ){
			    prefix += " Warlord's ";
			}
			else if( this.StrBonus == 4 ){
			    prefix += " Hero's ";
			}
			else if( this.StrBonus == 3 ){
			    prefix += " Champion's ";
			}
			else if( this.StrBonus == 2 ){
			    prefix += " Veteran's ";
			}
			else if( this.StrBonus == 1 ){
			    prefix += " Warrior's ";
			}
		    }

		    if( this.VirtualArmorMod > 0 ){
			if( this.VirtualArmorMod == 6 ){
			    prefix += "Meteoric Steel ";
			}
			if( this.VirtualArmorMod == 5 ){
			    prefix += "Adamantium ";
			}
			if( this.VirtualArmorMod == 4 ){
			    prefix += "Onyx ";
			}
			if( this.VirtualArmorMod == 3 ){
			    prefix += "Obsidian ";
			}
			if( this.VirtualArmorMod == 2 ){
			    prefix += "Steel ";
			}
			if( this.VirtualArmorMod == 1 ){
			    prefix += "Iron ";
			}
		    }
		}
		else {
		    //not identified or not staff
		    prefix = "unidentified ";
		}
                String text = prefix + Core.StringList.Table[this.LabelNumber];

		LabelTo( from, text );
                //LabelToAffix(from, LabelNumber, AffixType.Prepend, prefix);
            }
            else
            {
                base.OnSingleClick(from);
            }
	}

	public BaseJewel( Serial serial ) : base( serial )
	{
	}

	public override void GetProperties( ObjectPropertyList list )
	{
	    base.GetProperties( list );

	    m_AosSkillBonuses.GetProperties( list );

	    int prop;

	    if ( (prop = ArtifactRarity) > 0 )
		list.Add( 1061078, prop.ToString() ); // artifact rarity ~1_val~

	    if ( (prop = m_AosAttributes.WeaponDamage) != 0 )
		list.Add( 1060401, prop.ToString() ); // damage increase ~1_val~%

	    if ( (prop = m_AosAttributes.DefendChance) != 0 )
		list.Add( 1060408, prop.ToString() ); // defense chance increase ~1_val~%

	    if ( (prop = m_AosAttributes.BonusDex) != 0 )
		list.Add( 1060409, prop.ToString() ); // dexterity bonus ~1_val~

	    if ( (prop = m_AosAttributes.EnhancePotions) != 0 )
		list.Add( 1060411, prop.ToString() ); // enhance potions ~1_val~%

	    if ( (prop = m_AosAttributes.CastRecovery) != 0 )
		list.Add( 1060412, prop.ToString() ); // faster cast recovery ~1_val~

	    if ( (prop = m_AosAttributes.CastSpeed) != 0 )
		list.Add( 1060413, prop.ToString() ); // faster casting ~1_val~

	    if ( (prop = m_AosAttributes.AttackChance) != 0 )
		list.Add( 1060415, prop.ToString() ); // hit chance increase ~1_val~%

	    if ( (prop = m_AosAttributes.BonusHits) != 0 )
		list.Add( 1060431, prop.ToString() ); // hit point increase ~1_val~

	    if ( (prop = m_AosAttributes.BonusInt) != 0 )
		list.Add( 1060432, prop.ToString() ); // intelligence bonus ~1_val~

	    if ( (prop = m_AosAttributes.LowerManaCost) != 0 )
		list.Add( 1060433, prop.ToString() ); // lower mana cost ~1_val~%

	    if ( (prop = m_AosAttributes.LowerRegCost) != 0 )
		list.Add( 1060434, prop.ToString() ); // lower reagent cost ~1_val~%

	    if ( (prop = m_AosAttributes.Luck) != 0 )
		list.Add( 1060436, prop.ToString() ); // luck ~1_val~

	    if ( (prop = m_AosAttributes.BonusMana) != 0 )
		list.Add( 1060439, prop.ToString() ); // mana increase ~1_val~

	    if ( (prop = m_AosAttributes.RegenMana) != 0 )
		list.Add( 1060440, prop.ToString() ); // mana regeneration ~1_val~

	    if ( (prop = m_AosAttributes.NightSight) != 0 )
		list.Add( 1060441 ); // night sight

	    if ( (prop = m_AosAttributes.ReflectPhysical) != 0 )
		list.Add( 1060442, prop.ToString() ); // reflect physical damage ~1_val~%

	    if ( (prop = m_AosAttributes.RegenStam) != 0 )
		list.Add( 1060443, prop.ToString() ); // stamina regeneration ~1_val~

	    if ( (prop = m_AosAttributes.RegenHits) != 0 )
		list.Add( 1060444, prop.ToString() ); // hit point regeneration ~1_val~

	    if ( (prop = m_AosAttributes.SpellChanneling) != 0 )
		list.Add( 1060482 ); // spell channeling

	    if ( (prop = m_AosAttributes.SpellDamage) != 0 )
		list.Add( 1060483, prop.ToString() ); // spell damage increase ~1_val~%

	    if ( (prop = m_AosAttributes.BonusStam) != 0 )
		list.Add( 1060484, prop.ToString() ); // stamina increase ~1_val~

	    if ( (prop = m_AosAttributes.BonusStr) != 0 )
		list.Add( 1060485, prop.ToString() ); // strength bonus ~1_val~

	    if ( (prop = m_AosAttributes.WeaponSpeed) != 0 )
		list.Add( 1060486, prop.ToString() ); // swing speed increase ~1_val~%

	    if ( Core.ML && (prop = m_AosAttributes.IncreasedKarmaLoss) != 0 )
		list.Add( 1075210, prop.ToString() ); // Increased Karma Loss ~1val~%

	    base.AddResistanceProperties( list );

	    if ( m_HitPoints >= 0 && m_MaxHitPoints > 0 )
		list.Add( 1060639, "{0}\t{1}", m_HitPoints, m_MaxHitPoints ); // durability ~1_val~ / ~2_val~
	}

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );

	    writer.Write( (int) 5 ); // version

	    writer.Write( m_VirtualArmorMod );
	    m_ZuluSkillMods.Serialize( writer );
	    writer.Write( m_StrBonus );
	    writer.Write( m_DexBonus );
	    writer.Write( m_IntBonus );

	    writer.WriteEncodedInt( (int) m_MaxHitPoints );
	    writer.WriteEncodedInt( (int) m_HitPoints );

	    writer.WriteEncodedInt( (int) m_Resource );
	    writer.WriteEncodedInt( (int) m_GemType );

	    m_AosAttributes.Serialize( writer );
	    m_AosResistances.Serialize( writer );
	    m_AosSkillBonuses.Serialize( writer );
	}

	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );

	    int version = reader.ReadInt();

	    switch ( version )
	    {
		case 5:
		    {
			m_VirtualArmorMod = reader.ReadInt();
			if( Parent is Mobile ){
			    ((Mobile)Parent).VirtualArmorMod += m_VirtualArmorMod;
			}
			goto case 4;
		    }
		case 4:
		    {
			if( version == 4 ){
			    m_VirtualArmorMod = 0;
			}
			
			if( m_ZuluSkillMods == null ){
			    m_ZuluSkillMods = new ZuluSkillMods( this );
			}

			m_ZuluSkillMods.Deserialize( reader );

			m_StrBonus = reader.ReadInt();
			m_DexBonus = reader.ReadInt();
			m_IntBonus = reader.ReadInt();
			
			goto case 3;
		    }
		case 3:
		    {
			if( m_ZuluSkillMods == null ){
			    m_ZuluSkillMods = new ZuluSkillMods( this );
			}
			
			m_MaxHitPoints = reader.ReadEncodedInt();
			m_HitPoints = reader.ReadEncodedInt();

			goto case 2;
		    }
		case 2:
		    {
			m_Resource = (CraftResource)reader.ReadEncodedInt();
			m_GemType = (GemType)reader.ReadEncodedInt();

			goto case 1;
		    }
		case 1:
		    {
			m_AosAttributes = new AosAttributes( this, reader );
			m_AosResistances = new AosElementAttributes( this, reader );
			m_AosSkillBonuses = new AosSkillBonuses( this, reader );

			if ( Parent is Mobile ) {
			    //m_AosSkillBonuses.AddTo( (Mobile)Parent );
			    m_ZuluSkillMods.AddTo( (Mobile)Parent );
			}

			int strBonus = m_StrBonus;
			int dexBonus = m_DexBonus;
			int intBonus = m_IntBonus;

			if ( Parent is Mobile && (strBonus != 0 || dexBonus != 0 || intBonus != 0) )
			{
			    Mobile m = (Mobile)Parent;

			    string modName = Serial.ToString();

			    if ( strBonus != 0 )
				m.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

			    if ( dexBonus != 0 )
				m.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

			    if ( intBonus != 0 )
				m.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
			}

			if ( Parent is Mobile )
			    ((Mobile)Parent).CheckStatTimers();

			break;
		    }
		case 0:
		    {
			m_AosAttributes = new AosAttributes( this );
			m_AosResistances = new AosElementAttributes( this );
			m_AosSkillBonuses = new AosSkillBonuses( this );

			break;
		    }
	    }

	    if ( version < 2 )
	    {
		m_Resource = CraftResource.Iron;
		m_GemType = GemType.None;
	    }
	}
#region ICraftable Members

	public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
	{
	    Type resourceType = typeRes;

	    if ( resourceType == null )
		resourceType = craftItem.Resources.GetAt( 0 ).ItemType;

	    Resource = CraftResources.GetFromType( resourceType );

	    CraftContext context = craftSystem.GetContext( from );

	    if ( context != null && context.DoNotColor )
		Hue = 0;

	    if ( 1 < craftItem.Resources.Count )
	    {
		resourceType = craftItem.Resources.GetAt( 1 ).ItemType;

		if ( resourceType == typeof( StarSapphire ) )
		    GemType = GemType.StarSapphire;
		else if ( resourceType == typeof( Emerald ) )
		    GemType = GemType.Emerald;
		else if ( resourceType == typeof( Sapphire ) )
		    GemType = GemType.Sapphire;
		else if ( resourceType == typeof( Ruby ) )
		    GemType = GemType.Ruby;
		else if ( resourceType == typeof( Citrine ) )
		    GemType = GemType.Citrine;
		else if ( resourceType == typeof( Amethyst ) )
		    GemType = GemType.Amethyst;
		else if ( resourceType == typeof( Tourmaline ) )
		    GemType = GemType.Tourmaline;
		else if ( resourceType == typeof( Amber ) )
		    GemType = GemType.Amber;
		else if ( resourceType == typeof( Diamond ) )
		    GemType = GemType.Diamond;
	    }

	    return 1;
	}

#endregion
    }
}
