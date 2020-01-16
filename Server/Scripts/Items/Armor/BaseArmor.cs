using System;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;
using Server.Engines.Craft;
using Server.Factions;
using AMA = Server.Items.ArmorMeditationAllowance;
using AMT = Server.Items.ArmorMaterialType;
using ABT = Server.Items.ArmorBodyType;

namespace Server.Items
{
    public abstract class BaseArmor : Item, IScissorable, IFactionItem, ICraftable, IWearableDurability
    {
#region Factions
        private FactionItem m_FactionState;

        public FactionItem FactionItemState
        {
            get{ return m_FactionState; }
            set
            {
                m_FactionState = value;

                if ( m_FactionState == null )
                    Hue = CraftResources.GetHue( Resource );

                LootType = ( m_FactionState == null ? LootType.Regular : LootType.Blessed );
            }
        }
#endregion



        /* Armor internals work differently now (Jun 19 2003)
         *
         * The attributes defined below default to -1.
         * If the value is -1, the corresponding virtual 'Aos/Old' property is used.
         * If not, the attribute value itself is used. Here's the list:
         *  - ArmorBase
         *  - StrBonus
         *  - DexBonus
         *  - IntBonus
         *  - StrReq
         *  - DexReq
         *  - IntReq
         *  - MeditationAllowance
         */

        // Instance values. These values must are unique to each armor piece.
        private int m_MaxHitPoints;
        private int m_HitPoints;
        private Mobile m_Crafter;
        private ArmorQuality m_Quality;
        private ArmorDurabilityLevel m_Durability;
        private ArmorProtectionLevel m_Protection;
        private CraftResource m_Resource;
        private bool m_Identified, m_PlayerConstructed;
        private int m_PhysicalBonus, m_FireBonus, m_ColdBonus, m_PoisonBonus, m_EnergyBonus;

        private AosAttributes m_AosAttributes;
        private AosArmorAttributes m_AosArmorAttributes;
        private AosSkillBonuses m_AosSkillBonuses;

	private ZuluSkillMods m_ZuluSkillMods;
	
        // Overridable values. These values are provided to override the defaults which get defined in the individual armor scripts.
        private int m_ArmorBase = -1;
        private int m_StrBonus = -1, m_DexBonus = -1, m_IntBonus = -1;
        private int m_StrReq = -1, m_DexReq = -1, m_IntReq = -1;
        private AMA m_Meditate = (AMA)(-1);


        public virtual bool AllowMaleWearer{ get{ return true; } }
        public virtual bool AllowFemaleWearer{ get{ return true; } }

        public abstract AMT MaterialType{ get; }

        public virtual int RevertArmorBase{ get{ return ArmorBase; } }
        public virtual int ArmorBase{ get{ return 0; } }

        public virtual AMA DefMedAllowance{ get{ return AMA.None; } }
        public virtual AMA AosMedAllowance{ get{ return DefMedAllowance; } }
        public virtual AMA OldMedAllowance{ get{ return DefMedAllowance; } }


        public virtual int AosStrBonus{ get{ return 0; } }
        public virtual int AosDexBonus{ get{ return 0; } }
        public virtual int AosIntBonus{ get{ return 0; } }
        public virtual int AosStrReq{ get{ return 0; } }
        public virtual int AosDexReq{ get{ return 0; } }
        public virtual int AosIntReq{ get{ return 0; } }


        public virtual int OldStrBonus{ get{ return 0; } }
        public virtual int OldDexBonus{ get{ return 0; } }
        public virtual int OldIntBonus{ get{ return 0; } }
        public virtual int OldStrReq{ get{ return 0; } }
        public virtual int OldDexReq{ get{ return 0; } }
        public virtual int OldIntReq{ get{ return 0; } }

        public virtual bool CanFortify{ get{ return true; } }

        public override void OnAfterDuped( Item newItem )
        {
            BaseArmor armor = newItem as BaseArmor;

            if ( armor == null )
                return;

            armor.m_AosAttributes = new AosAttributes( newItem, m_AosAttributes );
            armor.m_AosArmorAttributes = new AosArmorAttributes( newItem, m_AosArmorAttributes );
            armor.m_AosSkillBonuses = new AosSkillBonuses( newItem, m_AosSkillBonuses );
	    armor.m_ZuluSkillMods = new ZuluSkillMods( newItem, m_ZuluSkillMods );
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public AMA MeditationAllowance
        {
            get{ return ( m_Meditate == (AMA)(-1) ? Core.AOS ? AosMedAllowance : OldMedAllowance : m_Meditate ); }
            set{ m_Meditate = value; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int BaseArmorRating
        {
            get
            {
                if ( m_ArmorBase == -1 )
                    return ArmorBase;
                else
                    return m_ArmorBase;
            }
            set
            {
                m_ArmorBase = value; Invalidate();
            }
        }

        public double BaseArmorRatingScaled
        {
            get
            {
                return ( BaseArmorRating * ArmorScalar );
            }
        }

        public virtual double ArmorRating
        {
            get
            {
		double ar = BaseArmorRating;

                if ( m_Protection != ArmorProtectionLevel.Regular )
                    ar += 10 + (double)(5 * (int)m_Protection);

                // TOGENERATE
                switch ( m_Resource )
                {
                    case CraftResource.Iron: ar *= 1.00; break;
                    case CraftResource.Gold: ar *=  1.00; break;
                    case CraftResource.Spike: ar *=  1.05; break;
                    case CraftResource.Fruity: ar *=  1.05; break;
                    case CraftResource.Bronze: ar *=  1.10; break;
                    case CraftResource.IceRock: ar *=  1.05; break;
                    case CraftResource.BlackDwarf: ar *=  1.10; break;
                    case CraftResource.DullCopper: ar *=  1.15; break;
                    case CraftResource.Platinum: ar *=  1.15; break;
                    case CraftResource.SilverRock: ar *=  1.10; break;
                    case CraftResource.DarkPagan: ar *=  1.15; break;
                    case CraftResource.Copper: ar *=  1.20; break;
                    case CraftResource.Mystic: ar *=  1.05; break;
                    case CraftResource.Spectral: ar *=  1.10; break;
                    case CraftResource.OldBritain: ar *=  1.25; break;
                    case CraftResource.Onyx: ar *=  1.25; break;
                    case CraftResource.RedElven: ar *=  1.15; break;
                    case CraftResource.Undead: ar *=  1.20; break;
                    case CraftResource.Pyrite: ar *=  1.30; break;
                    case CraftResource.Virginity: ar *=  1.25; break;
                    case CraftResource.Malachite: ar *=  1.50; break;
                    case CraftResource.Lavarock: ar *=  1.35; break;
                    case CraftResource.Azurite: ar *=  1.50; break;
                    case CraftResource.Dripstone: ar *=  1.60; break;
                    case CraftResource.Executor: ar *=  1.60; break;
                    case CraftResource.Peachblue: ar *=  1.70; break;
                    case CraftResource.Destruction: ar *=  1.70; break;
                    case CraftResource.Anra: ar *=  1.80; break;
                    case CraftResource.Crystal: ar *=  1.80; break;
                    case CraftResource.Doom: ar *=  1.90; break;
                    case CraftResource.Goddess: ar *=  1.90; break;
                    case CraftResource.NewZulu: ar *=  2.00; break;
                    case CraftResource.EbonTwilightSapphire: ar *=  2.20; break;
                    case CraftResource.DarkSableRuby: ar *=  2.20; break;
                    case CraftResource.RadiantNimbusDiamond: ar *=  2.25; break;
			
                    case CraftResource.SpinedLeather:	ar *=  1.0; break;
                    case CraftResource.HornedLeather:	ar *=  1.05; break;
                    case CraftResource.BarbedLeather:	ar *=  1.10; break;
		    case CraftResource.RatLeather: ar *=  1.15; break;
		    case CraftResource.WolfLeather: ar *=  1.20; break;
		    case CraftResource.BearLeather: ar *=  1.25; break;
		    case CraftResource.SerpentLeather: ar *=  1.30; break;
		    case CraftResource.LizardLeather: ar *=  1.35; break;
		    case CraftResource.TrollLeather: ar *=  1.40; break;
		    case CraftResource.OstardLeather: ar *=  1.45; break;
		    case CraftResource.NecromancerLeather: ar *=  1.50; break;
		    case CraftResource.LavaLeather: ar *=  1.55; break;
		    case CraftResource.LicheLeather: ar *=  1.60; break;
		    case CraftResource.IceCrystalLeather: ar *=  1.65; break;
		    case CraftResource.DragonLeather: ar *=  1.70; break;
		    case CraftResource.WyrmLeather: ar *=  1.80; break;
		    case CraftResource.BalronLeather: ar *=  1.90; break;
		    case CraftResource.GoldenDragonLeather: ar *=  2.0; break;
			
		    case CraftResource.Pinetree: ar *=  1.05; break;
		    case CraftResource.Cherry: ar *=  1.10; break;
		    case CraftResource.Oak: ar *=  1.15; break;
		    case CraftResource.PurplePassion: ar *=  1.20; break;
		    case CraftResource.GoldenReflection: ar *=  1.25; break;
		    case CraftResource.Hardranger: ar *=  1.25; break;
		    case CraftResource.Jadewood: ar *=  1.30; break;
		    case CraftResource.Darkwood: ar *=  1.35; break;
		    case CraftResource.Stonewood: ar *=  1.40; break;
		    case CraftResource.Sunwood: ar *=  1.45; break;
		    case CraftResource.Gauntlet: ar *=  1.45; break;
		    case CraftResource.Swampwood: ar *=  1.50; break;
		    case CraftResource.Stardust: ar *=  1.55; break;
		    case CraftResource.Silverleaf: ar *=  1.60; break;
		    case CraftResource.Stormteal: ar *=  1.65; break;
		    case CraftResource.Emeraldwood: ar *=  1.70; break;
		    case CraftResource.Bloodwood: ar *=  1.75; break;
		    case CraftResource.Crystalwood: ar *=  1.80; break;
		    case CraftResource.Bloodhorse: ar *=  1.85; break;
		    case CraftResource.Doomwood: ar *=  1.90; break;
		    case CraftResource.Zulu: ar *=  2.00; break;
		    case CraftResource.Darkness: ar *=  2.05; break;
		    case CraftResource.Elven: ar *=  2.10; break;
                }

		ar += -8 + (double)(8 * (int)m_Quality);

		if( Parent is PlayerMobile ){
		    PlayerMobile pm = Parent as PlayerMobile;
		    if( pm.Spec.SpecName == SpecName.Warrior ){
			ar *= pm.Spec.Bonus;
		    }
		}
		
                return ScaleArmorByDurability( (int)ar );
            }
        }

        public double ArmorRatingScaled
        {
            get
            {
                return ( ArmorRating * ArmorScalar );
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int StrBonus
        {
            get{ return ( m_StrBonus == -1 ? Core.AOS ? AosStrBonus : OldStrBonus : m_StrBonus ); }
            set{ m_StrBonus = value; InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int DexBonus
        {
            get{ return ( m_DexBonus == -1 ? Core.AOS ? AosDexBonus : OldDexBonus : m_DexBonus ); }
            set{ m_DexBonus = value; InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int IntBonus
        {
            get{ return ( m_IntBonus == -1 ? Core.AOS ? AosIntBonus : OldIntBonus : m_IntBonus ); }
            set{ m_IntBonus = value; InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int StrRequirement
        {
            get{ return ( m_StrReq == -1 ? Core.AOS ? AosStrReq : OldStrReq : m_StrReq ); }
            set{ m_StrReq = value; InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int DexRequirement
        {
            get{ return ( m_DexReq == -1 ? Core.AOS ? AosDexReq : OldDexReq : m_DexReq ); }
            set{ m_DexReq = value; InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int IntRequirement
        {
            get{ return ( m_IntReq == -1 ? Core.AOS ? AosIntReq : OldIntReq : m_IntReq ); }
            set{ m_IntReq = value; InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public bool Identified
        {
            get{ return m_Identified; }
            set{ m_Identified = value; InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public bool PlayerConstructed
        {
            get{ return m_PlayerConstructed; }
            set{ m_PlayerConstructed = value; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public CraftResource Resource
        {
            get
            {
                return m_Resource;
            }
            set
            {
                if ( m_Resource != value )
                {
                    UnscaleDurability();

                    m_Resource = value;

                    if ( CraftItem.RetainsColor( this.GetType() ) )
                    {
                        Hue = CraftResources.GetHue(m_Resource);
                    }

                    Invalidate();
                    InvalidateProperties();

                    if ( Parent is Mobile )
                        ((Mobile)Parent).UpdateResistances();

                    ScaleDurability();
                }
            }
        }

        public virtual double ArmorScalar
        {
            get
            {
                int pos = (int)BodyPosition;

                if ( pos >= 0 && pos < m_ArmorScalars.Length )
                    return m_ArmorScalars[pos];

                return 1.0;
            }
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


        [CommandProperty( AccessLevel.GameMaster )]
        public Mobile Crafter
        {
            get{ return m_Crafter; }
            set{ m_Crafter = value; InvalidateProperties(); }
        }


        [CommandProperty( AccessLevel.GameMaster )]
        public ArmorQuality Quality
        {
            get{ return m_Quality; }
            set{ UnscaleDurability(); m_Quality = value; Invalidate(); InvalidateProperties(); ScaleDurability(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public ArmorDurabilityLevel Durability
        {
            get{ return m_Durability; }
            set{ UnscaleDurability(); m_Durability = value; ScaleDurability(); InvalidateProperties(); }
        }

        public virtual int ArtifactRarity
        {
            get{ return 0; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public ArmorProtectionLevel ProtectionLevel
        {
            get
            {
                return m_Protection;
            }
            set
            {
                if ( m_Protection != value )
                {
                    m_Protection = value;

                    Invalidate();
                    InvalidateProperties();

                    if ( Parent is Mobile )
                        ((Mobile)Parent).UpdateResistances();
                }
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public AosAttributes Attributes
        {
            get{ return m_AosAttributes; }
            set{}
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public AosArmorAttributes ArmorAttributes
        {
            get{ return m_AosArmorAttributes; }
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

        public int ComputeStatReq( StatType type )
        {
            int v;

            if ( type == StatType.Str )
                v = StrRequirement;
            else if ( type == StatType.Dex )
                v = DexRequirement;
            else
                v = IntRequirement;

            return AOS.Scale( v, 100 - GetLowerStatReq() );
        }

        public int ComputeStatBonus( StatType type )
        {
            if ( type == StatType.Str )
                return StrBonus + Attributes.BonusStr;
            else if ( type == StatType.Dex )
                return DexBonus + Attributes.BonusDex;
            else
                return IntBonus + Attributes.BonusInt;
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int PhysicalBonus{ get{ return m_PhysicalBonus; } set{ m_PhysicalBonus = value; InvalidateProperties(); } }

        [CommandProperty( AccessLevel.GameMaster )]
        public int FireBonus{ get{ return m_FireBonus; } set{ m_FireBonus = value; InvalidateProperties(); } }

        [CommandProperty( AccessLevel.GameMaster )]
        public int ColdBonus{ get{ return m_ColdBonus; } set{ m_ColdBonus = value; InvalidateProperties(); } }

        [CommandProperty( AccessLevel.GameMaster )]
        public int PoisonBonus{ get{ return m_PoisonBonus; } set{ m_PoisonBonus = value; InvalidateProperties(); } }

        [CommandProperty( AccessLevel.GameMaster )]
        public int EnergyBonus{ get{ return m_EnergyBonus; } set{ m_EnergyBonus = value; InvalidateProperties(); } }

        public virtual int BasePhysicalResistance{ get{ return 0; } }
        public virtual int BaseFireResistance{ get{ return 0; } }
        public virtual int BaseColdResistance{ get{ return 0; } }
        public virtual int BasePoisonResistance{ get{ return 0; } }
        public virtual int BaseEnergyResistance{ get{ return 0; } }

        public override int PhysicalResistance{ get{ return BasePhysicalResistance + GetProtOffset() + GetResourceAttrs().ArmorPhysicalResist + m_PhysicalBonus; } }
        public override int FireResistance{ get{ return BaseFireResistance + GetProtOffset() + GetResourceAttrs().ArmorFireResist + m_FireBonus; } }
        public override int ColdResistance{ get{ return BaseColdResistance + GetProtOffset() + GetResourceAttrs().ArmorColdResist + m_ColdBonus; } }
        public override int PoisonResistance{ get{ return BasePoisonResistance + GetProtOffset() + GetResourceAttrs().ArmorPoisonResist + m_PoisonBonus; } }
        public override int EnergyResistance{ get{ return BaseEnergyResistance + GetProtOffset() + GetResourceAttrs().ArmorEnergyResist + m_EnergyBonus; } }

        public virtual int InitMinHits{ get{ return 0; } }
        public virtual int InitMaxHits{ get{ return 0; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public ArmorBodyType BodyPosition
        {
            get
            {
                switch ( this.Layer )
                {
                    default:
                    case Layer.Neck:		return ArmorBodyType.Gorget;
                    case Layer.TwoHanded:	return ArmorBodyType.Shield;
                    case Layer.Gloves:		return ArmorBodyType.Gloves;
                    case Layer.Helm:		return ArmorBodyType.Helmet;
                    case Layer.Arms:		return ArmorBodyType.Arms;

                    case Layer.InnerLegs:
                    case Layer.OuterLegs:
                    case Layer.Pants:		return ArmorBodyType.Legs;

                    case Layer.InnerTorso:
                    case Layer.OuterTorso:
                    case Layer.Shirt:		return ArmorBodyType.Chest;
                }
            }
        }

        public void DistributeBonuses( int amount )
        {
            for ( int i = 0; i < amount; ++i )
            {
                switch ( Utility.Random( 5 ) )
                {
                    case 0: ++m_PhysicalBonus; break;
                    case 1: ++m_FireBonus; break;
                    case 2: ++m_ColdBonus; break;
                    case 3: ++m_PoisonBonus; break;
                    case 4: ++m_EnergyBonus; break;
                }
            }

            InvalidateProperties();
        }

        public CraftAttributeInfo GetResourceAttrs()
        {
            CraftResourceInfo info = CraftResources.GetInfo( m_Resource );

            if ( info == null )
                return CraftAttributeInfo.Blank;

            return info.AttributeInfo;
        }

        public int GetProtOffset()
        {
            switch ( m_Protection )
            {
                case ArmorProtectionLevel.Guarding: return 1;
                case ArmorProtectionLevel.Hardening: return 2;
                case ArmorProtectionLevel.Fortification: return 3;
                case ArmorProtectionLevel.Invulnerability: return 4;
            }

            return 0;
        }

        public void UnscaleDurability()
        {
            int scale = 100 + GetDurabilityBonus();

            m_HitPoints = ((m_HitPoints * 100) + (scale - 1)) / scale;
            m_MaxHitPoints = ((m_MaxHitPoints * 100) + (scale - 1)) / scale;
            InvalidateProperties();
        }

        public void ScaleDurability()
        {
            int scale = 100 + GetDurabilityBonus();

            m_HitPoints = ((m_HitPoints * scale) + 99) / 100;
            m_MaxHitPoints = ((m_MaxHitPoints * scale) + 99) / 100;
            InvalidateProperties();
        }

        public int GetDurabilityBonus()
        {
            int bonus = 0;

            if ( m_Quality == ArmorQuality.Exceptional )
                bonus += 20;

            switch ( m_Durability )
            {
                case ArmorDurabilityLevel.Durable: bonus += 20; break;
                case ArmorDurabilityLevel.Substantial: bonus += 50; break;
                case ArmorDurabilityLevel.Massive: bonus += 70; break;
                case ArmorDurabilityLevel.Fortified: bonus += 100; break;
                case ArmorDurabilityLevel.Indestructible: bonus += 120; break;
            }

            if ( Core.AOS )
            {
                bonus += m_AosArmorAttributes.DurabilityBonus;

                CraftResourceInfo resInfo = CraftResources.GetInfo( m_Resource );
                CraftAttributeInfo attrInfo = null;

                if ( resInfo != null )
                    attrInfo = resInfo.AttributeInfo;

                if ( attrInfo != null )
                    bonus += attrInfo.ArmorDurability;
            }

            return bonus;
        }

        public bool Scissor( Mobile from, Scissors scissors )
        {
            if ( !IsChildOf( from.Backpack ) )
            {
                from.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
                return false;
            }

            if ( Ethics.Ethic.IsImbued( this ) )
            {
                from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
                return false;
            }

            CraftSystem system = DefTailoring.CraftSystem;

            CraftItem item = system.CraftItems.SearchFor( GetType() );

            if ( item != null && item.Resources.Count == 1 && item.Resources.GetAt( 0 ).Amount >= 2 )
            {
                try
                {
                    Item res = (Item)Activator.CreateInstance( CraftResources.GetInfo( m_Resource ).ResourceTypes[0] );

                    ScissorHelper( from, res, m_PlayerConstructed ? (item.Resources.GetAt( 0 ).Amount / 2) : 1 );
                    return true;
                }
                catch
                {
                }
            }

            from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
            return false;
        }

        private static double[] m_ArmorScalars = { 0.07, 0.07, 0.14, 0.15, 0.22, 0.35 };

        public static double[] ArmorScalars
        {
            get
            {
                return m_ArmorScalars;
            }
            set
            {
                m_ArmorScalars = value;
            }
        }

        public static void ValidateMobile( Mobile m )
        {
            for ( int i = m.Items.Count - 1; i >= 0; --i )
            {
                if ( i >= m.Items.Count )
                    continue;

                Item item = m.Items[i];

                if ( item is BaseArmor )
                {
                    BaseArmor armor = (BaseArmor)item;

                    if( armor.RequiredRace != null && m.Race != armor.RequiredRace )
                    {
                        if( armor.RequiredRace == Race.Elf )
                            m.SendLocalizedMessage( 1072203 ); // Only Elves may use this.
                        else
                            m.SendMessage( "Only {0} may use this.", armor.RequiredRace.PluralName );

                        m.AddToBackpack( armor );
                    }
                    else if ( !armor.AllowMaleWearer && !m.Female && m.AccessLevel < AccessLevel.GameMaster )
                    {
                        if ( armor.AllowFemaleWearer )
                            m.SendLocalizedMessage( 1010388 ); // Only females can wear this.
                        else
                            m.SendMessage( "You may not wear this." );

                        m.AddToBackpack( armor );
                    }
                    else if ( !armor.AllowFemaleWearer && m.Female && m.AccessLevel < AccessLevel.GameMaster )
                    {
                        if ( armor.AllowMaleWearer )
                            m.SendLocalizedMessage( 1063343 ); // Only males can wear this.
                        else
                            m.SendMessage( "You may not wear this." );

                        m.AddToBackpack( armor );
                    }
                }
            }
        }

        public int GetLowerStatReq()
        {
            if ( !Core.AOS )
                return 0;

            int v = m_AosArmorAttributes.LowerStatReq;

            CraftResourceInfo info = CraftResources.GetInfo( m_Resource );

            if ( info != null )
            {
                CraftAttributeInfo attrInfo = info.AttributeInfo;

                if ( attrInfo != null )
                    v += attrInfo.ArmorLowerRequirements;
            }

            if ( v > 100 )
                v = 100;

            return v;
        }

        public override void OnAdded(IEntity parent)
        {
            if ( parent is Mobile )
            {
                Mobile from = (Mobile)parent;

                //if ( Core.AOS )
                //    m_AosSkillBonuses.AddTo( from );

		m_ZuluSkillMods.AddTo( from );
		
                from.Delta( MobileDelta.Armor ); // Tell them armor rating has changed
            }
        }

        public virtual double ScaleArmorByDurability( double armor )
        {
            int scale = 100;

            if ( m_MaxHitPoints > 0 && m_HitPoints < m_MaxHitPoints )
                scale = 50 + ((50 * m_HitPoints) / m_MaxHitPoints);

            return ( armor * scale ) / 100;
        }

        protected void Invalidate()
        {
            if ( Parent is Mobile )
                ((Mobile)Parent).Delta( MobileDelta.Armor ); // Tell them armor rating has changed
        }

        public BaseArmor( Serial serial ) :  base( serial )
        {
        }

        private static void SetSaveFlag( ref SaveFlag flags, SaveFlag toSet, bool setIf )
        {
            if ( setIf )
                flags |= toSet;
        }

        private static bool GetSaveFlag( SaveFlag flags, SaveFlag toGet )
        {
            return ( (flags & toGet) != 0 );
        }

        [Flags]
        private enum SaveFlag
	{
	    None				= 0x00000000,
	    Attributes			= 0x00000001,
	    ArmorAttributes		= 0x00000002,
	    PhysicalBonus		= 0x00000004,
	    FireBonus			= 0x00000008,
	    ColdBonus			= 0x00000010,
	    PoisonBonus			= 0x00000020,
	    EnergyBonus			= 0x00000040,
	    Identified			= 0x00000080,
	    MaxHitPoints		= 0x00000100,
	    HitPoints			= 0x00000200,
	    Crafter				= 0x00000400,
	    Quality				= 0x00000800,
	    Durability			= 0x00001000,
	    Protection			= 0x00002000,
	    Resource			= 0x00004000,
	    BaseArmor			= 0x00008000,
	    StrBonus			= 0x00010000,
	    DexBonus			= 0x00020000,
	    IntBonus			= 0x00040000,
	    StrReq				= 0x00080000,
	    DexReq				= 0x00100000,
	    IntReq				= 0x00200000,
	    MedAllowance		= 0x00400000,
	    SkillBonuses		= 0x00800000,
	    PlayerConstructed	= 0x01000000
	}

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 8 ); // version

	    m_ZuluSkillMods.Serialize(writer);
	    
            SaveFlag flags = SaveFlag.None;

            SetSaveFlag( ref flags, SaveFlag.Attributes,		!m_AosAttributes.IsEmpty );
            SetSaveFlag( ref flags, SaveFlag.ArmorAttributes,	!m_AosArmorAttributes.IsEmpty );
            SetSaveFlag( ref flags, SaveFlag.PhysicalBonus,		m_PhysicalBonus != 0 );
            SetSaveFlag( ref flags, SaveFlag.FireBonus,			m_FireBonus != 0 );
            SetSaveFlag( ref flags, SaveFlag.ColdBonus,			m_ColdBonus != 0 );
            SetSaveFlag( ref flags, SaveFlag.PoisonBonus,		m_PoisonBonus != 0 );
            SetSaveFlag( ref flags, SaveFlag.EnergyBonus,		m_EnergyBonus != 0 );
            SetSaveFlag( ref flags, SaveFlag.Identified,		m_Identified != false );
            SetSaveFlag( ref flags, SaveFlag.MaxHitPoints,		m_MaxHitPoints != 0 );
            SetSaveFlag( ref flags, SaveFlag.HitPoints,			m_HitPoints != 0 );
            SetSaveFlag( ref flags, SaveFlag.Crafter,			m_Crafter != null );
            SetSaveFlag( ref flags, SaveFlag.Quality,			m_Quality != ArmorQuality.Regular );
            SetSaveFlag( ref flags, SaveFlag.Durability,		m_Durability != ArmorDurabilityLevel.Regular );
            SetSaveFlag( ref flags, SaveFlag.Protection,		m_Protection != ArmorProtectionLevel.Regular );
            SetSaveFlag( ref flags, SaveFlag.Resource,			m_Resource != DefaultResource );
            SetSaveFlag( ref flags, SaveFlag.BaseArmor,			m_ArmorBase != -1 );
            SetSaveFlag( ref flags, SaveFlag.StrBonus,			m_StrBonus != -1 );
            SetSaveFlag( ref flags, SaveFlag.DexBonus,			m_DexBonus != -1 );
            SetSaveFlag( ref flags, SaveFlag.IntBonus,			m_IntBonus != -1 );
            SetSaveFlag( ref flags, SaveFlag.StrReq,			m_StrReq != -1 );
            SetSaveFlag( ref flags, SaveFlag.DexReq,			m_DexReq != -1 );
            SetSaveFlag( ref flags, SaveFlag.IntReq,			m_IntReq != -1 );
            SetSaveFlag( ref flags, SaveFlag.MedAllowance,		m_Meditate != (AMA)(-1) );
            SetSaveFlag( ref flags, SaveFlag.SkillBonuses,		!m_AosSkillBonuses.IsEmpty );
            SetSaveFlag( ref flags, SaveFlag.PlayerConstructed,	m_PlayerConstructed != false );

            writer.WriteEncodedInt( (int) flags );

            if ( GetSaveFlag( flags, SaveFlag.Attributes ) )
                m_AosAttributes.Serialize( writer );

            if ( GetSaveFlag( flags, SaveFlag.ArmorAttributes ) )
                m_AosArmorAttributes.Serialize( writer );

            if ( GetSaveFlag( flags, SaveFlag.PhysicalBonus ) )
                writer.WriteEncodedInt( (int) m_PhysicalBonus );

            if ( GetSaveFlag( flags, SaveFlag.FireBonus ) )
                writer.WriteEncodedInt( (int) m_FireBonus );

            if ( GetSaveFlag( flags, SaveFlag.ColdBonus ) )
                writer.WriteEncodedInt( (int) m_ColdBonus );

            if ( GetSaveFlag( flags, SaveFlag.PoisonBonus ) )
                writer.WriteEncodedInt( (int) m_PoisonBonus );

            if ( GetSaveFlag( flags, SaveFlag.EnergyBonus ) )
                writer.WriteEncodedInt( (int) m_EnergyBonus );

            if ( GetSaveFlag( flags, SaveFlag.MaxHitPoints ) )
                writer.WriteEncodedInt( (int) m_MaxHitPoints );

            if ( GetSaveFlag( flags, SaveFlag.HitPoints ) )
                writer.WriteEncodedInt( (int) m_HitPoints );

            if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
                writer.Write( (Mobile) m_Crafter );

            if ( GetSaveFlag( flags, SaveFlag.Quality ) )
                writer.WriteEncodedInt( (int) m_Quality );

            if ( GetSaveFlag( flags, SaveFlag.Durability ) )
                writer.WriteEncodedInt( (int) m_Durability );

            if ( GetSaveFlag( flags, SaveFlag.Protection ) )
                writer.WriteEncodedInt( (int) m_Protection );

            if ( GetSaveFlag( flags, SaveFlag.Resource ) )
                writer.WriteEncodedInt( (int) m_Resource );

            if ( GetSaveFlag( flags, SaveFlag.BaseArmor ) )
                writer.WriteEncodedInt( (int) m_ArmorBase );

            if ( GetSaveFlag( flags, SaveFlag.StrBonus ) )
                writer.WriteEncodedInt( (int) m_StrBonus );

            if ( GetSaveFlag( flags, SaveFlag.DexBonus ) )
                writer.WriteEncodedInt( (int) m_DexBonus );

            if ( GetSaveFlag( flags, SaveFlag.IntBonus ) )
                writer.WriteEncodedInt( (int) m_IntBonus );

            if ( GetSaveFlag( flags, SaveFlag.StrReq ) )
                writer.WriteEncodedInt( (int) m_StrReq );

            if ( GetSaveFlag( flags, SaveFlag.DexReq ) )
                writer.WriteEncodedInt( (int) m_DexReq );

            if ( GetSaveFlag( flags, SaveFlag.IntReq ) )
                writer.WriteEncodedInt( (int) m_IntReq );

            if ( GetSaveFlag( flags, SaveFlag.MedAllowance ) )
                writer.WriteEncodedInt( (int) m_Meditate );

            if ( GetSaveFlag( flags, SaveFlag.SkillBonuses ) )
                m_AosSkillBonuses.Serialize( writer );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            switch ( version )
            {
		case 8:
		    {
			if( m_ZuluSkillMods == null ){
			    m_ZuluSkillMods = new ZuluSkillMods( this );
			}

			m_ZuluSkillMods.Deserialize( reader );
			goto case 7;
		    }
                case 7:
                case 6:
                case 5:
                    {
			if( m_ZuluSkillMods == null ){
			    m_ZuluSkillMods = new ZuluSkillMods( this );
			}
			
                        SaveFlag flags = (SaveFlag)reader.ReadEncodedInt();

                        if ( GetSaveFlag( flags, SaveFlag.Attributes ) )
                            m_AosAttributes = new AosAttributes( this, reader );
                        else
                            m_AosAttributes = new AosAttributes( this );

                        if ( GetSaveFlag( flags, SaveFlag.ArmorAttributes ) )
                            m_AosArmorAttributes = new AosArmorAttributes( this, reader );
                        else
                            m_AosArmorAttributes = new AosArmorAttributes( this );

                        if ( GetSaveFlag( flags, SaveFlag.PhysicalBonus ) )
                            m_PhysicalBonus = reader.ReadEncodedInt();

                        if ( GetSaveFlag( flags, SaveFlag.FireBonus ) )
                            m_FireBonus = reader.ReadEncodedInt();

                        if ( GetSaveFlag( flags, SaveFlag.ColdBonus ) )
                            m_ColdBonus = reader.ReadEncodedInt();

                        if ( GetSaveFlag( flags, SaveFlag.PoisonBonus ) )
                            m_PoisonBonus = reader.ReadEncodedInt();

                        if ( GetSaveFlag( flags, SaveFlag.EnergyBonus ) )
                            m_EnergyBonus = reader.ReadEncodedInt();

                        if ( GetSaveFlag( flags, SaveFlag.Identified ) )
                            m_Identified = ( version >= 7 || reader.ReadBool() );

                        if ( GetSaveFlag( flags, SaveFlag.MaxHitPoints ) )
                            m_MaxHitPoints = reader.ReadEncodedInt();

                        if ( GetSaveFlag( flags, SaveFlag.HitPoints ) )
                            m_HitPoints = reader.ReadEncodedInt();

                        if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
                            m_Crafter = reader.ReadMobile();

                        if ( GetSaveFlag( flags, SaveFlag.Quality ) )
                            m_Quality = (ArmorQuality)reader.ReadEncodedInt();
                        else
                            m_Quality = ArmorQuality.Regular;

                        if ( version == 5 && m_Quality == ArmorQuality.Low )
                            m_Quality = ArmorQuality.Regular;

                        if ( GetSaveFlag( flags, SaveFlag.Durability ) )
                        {
                            m_Durability = (ArmorDurabilityLevel)reader.ReadEncodedInt();

                            if ( m_Durability > ArmorDurabilityLevel.Indestructible )
                                m_Durability = ArmorDurabilityLevel.Durable;
                        }

                        if ( GetSaveFlag( flags, SaveFlag.Protection ) )
                        {
                            m_Protection = (ArmorProtectionLevel)reader.ReadEncodedInt();

                            if ( m_Protection > ArmorProtectionLevel.Invulnerability )
                                m_Protection = ArmorProtectionLevel.Defense;
                        }

                        if ( GetSaveFlag( flags, SaveFlag.Resource ) )
                            m_Resource = (CraftResource)reader.ReadEncodedInt();
                        else
                            m_Resource = DefaultResource;

                        if ( m_Resource == CraftResource.None )
                            m_Resource = DefaultResource;

                        if ( GetSaveFlag( flags, SaveFlag.BaseArmor ) )
                            m_ArmorBase = reader.ReadEncodedInt();
                        else
                            m_ArmorBase = -1;

                        if ( GetSaveFlag( flags, SaveFlag.StrBonus ) )
                            m_StrBonus = reader.ReadEncodedInt();
                        else
                            m_StrBonus = -1;

                        if ( GetSaveFlag( flags, SaveFlag.DexBonus ) )
                            m_DexBonus = reader.ReadEncodedInt();
                        else
                            m_DexBonus = -1;

                        if ( GetSaveFlag( flags, SaveFlag.IntBonus ) )
                            m_IntBonus = reader.ReadEncodedInt();
                        else
                            m_IntBonus = -1;

                        if ( GetSaveFlag( flags, SaveFlag.StrReq ) )
                            m_StrReq = reader.ReadEncodedInt();
                        else
                            m_StrReq = -1;

                        if ( GetSaveFlag( flags, SaveFlag.DexReq ) )
                            m_DexReq = reader.ReadEncodedInt();
                        else
                            m_DexReq = -1;

                        if ( GetSaveFlag( flags, SaveFlag.IntReq ) )
                            m_IntReq = reader.ReadEncodedInt();
                        else
                            m_IntReq = -1;

                        if ( GetSaveFlag( flags, SaveFlag.MedAllowance ) )
                            m_Meditate = (AMA)reader.ReadEncodedInt();
                        else
                            m_Meditate = (AMA)(-1);

                        if ( GetSaveFlag( flags, SaveFlag.SkillBonuses ) )
                            m_AosSkillBonuses = new AosSkillBonuses( this, reader );

                        if ( GetSaveFlag( flags, SaveFlag.PlayerConstructed ) )
                            m_PlayerConstructed = true;

                        break;
                    }
                case 4:
                    {
                        m_AosAttributes = new AosAttributes( this, reader );
                        m_AosArmorAttributes = new AosArmorAttributes( this, reader );
                        goto case 3;
                    }
                case 3:
                    {
                        m_PhysicalBonus = reader.ReadInt();
                        m_FireBonus = reader.ReadInt();
                        m_ColdBonus = reader.ReadInt();
                        m_PoisonBonus = reader.ReadInt();
                        m_EnergyBonus = reader.ReadInt();
                        goto case 2;
                    }
                case 2:
                case 1:
                    {
                        m_Identified = reader.ReadBool();
                        goto case 0;
                    }
                case 0:
                    {
                        m_ArmorBase = reader.ReadInt();
                        m_MaxHitPoints = reader.ReadInt();
                        m_HitPoints = reader.ReadInt();
                        m_Crafter = reader.ReadMobile();
                        m_Quality = (ArmorQuality)reader.ReadInt();
                        m_Durability = (ArmorDurabilityLevel)reader.ReadInt();
                        m_Protection = (ArmorProtectionLevel)reader.ReadInt();

                        AMT mat = (AMT)reader.ReadInt();

                        if ( m_ArmorBase == RevertArmorBase )
                            m_ArmorBase = -1;

                        /*m_BodyPos = (ArmorBodyType)*/reader.ReadInt();

                        if ( version < 4 )
                        {
                            m_AosAttributes = new AosAttributes( this );
                            m_AosArmorAttributes = new AosArmorAttributes( this );
                        }

                        if ( version < 3 && m_Quality == ArmorQuality.Exceptional )
                            DistributeBonuses( 6 );

                        if ( version >= 2 )
                        {
                            m_Resource = (CraftResource)reader.ReadInt();
                        }
                        else
                        {
                            throw new System.Exception("Unsupported old item format.");
                        }

                        m_StrBonus = reader.ReadInt();
                        m_DexBonus = reader.ReadInt();
                        m_IntBonus = reader.ReadInt();
                        m_StrReq = reader.ReadInt();
                        m_DexReq = reader.ReadInt();
                        m_IntReq = reader.ReadInt();

                        if ( m_StrBonus == OldStrBonus )
                            m_StrBonus = -1;

                        if ( m_DexBonus == OldDexBonus )
                            m_DexBonus = -1;

                        if ( m_IntBonus == OldIntBonus )
                            m_IntBonus = -1;

                        if ( m_StrReq == OldStrReq )
                            m_StrReq = -1;

                        if ( m_DexReq == OldDexReq )
                            m_DexReq = -1;

                        if ( m_IntReq == OldIntReq )
                            m_IntReq = -1;

                        m_Meditate = (AMA)reader.ReadInt();

                        if ( m_Meditate == OldMedAllowance )
                            m_Meditate = (AMA)(-1);

                        if ( m_Resource == CraftResource.None )
                        {
                            if ( mat == ArmorMaterialType.Studded || mat == ArmorMaterialType.Leather )
                                m_Resource = CraftResource.RegularLeather;
                            else if ( mat == ArmorMaterialType.Spined )
                                m_Resource = CraftResource.SpinedLeather;
                            else if ( mat == ArmorMaterialType.Horned )
                                m_Resource = CraftResource.HornedLeather;
                            else if ( mat == ArmorMaterialType.Barbed )
                                m_Resource = CraftResource.BarbedLeather;
                            else
                                m_Resource = CraftResource.Iron;
                        }

                        if ( m_MaxHitPoints == 0 && m_HitPoints == 0 )
                            m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax( InitMinHits, InitMaxHits );

                        break;
                    }
            }

            if ( m_AosSkillBonuses == null )
                m_AosSkillBonuses = new AosSkillBonuses( this );

            //if ( Core.AOS && Parent is Mobile )
            //    m_AosSkillBonuses.AddTo( (Mobile)Parent );

	    if( Parent is Mobile ){
		m_ZuluSkillMods.AddTo( (Mobile)Parent );
	    }

            int strBonus = ComputeStatBonus( StatType.Str );
            int dexBonus = ComputeStatBonus( StatType.Dex );
            int intBonus = ComputeStatBonus( StatType.Int );

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

            if ( version < 7 )
                m_PlayerConstructed = true; // we don't know, so, assume it's crafted
        }

        public virtual CraftResource DefaultResource{ get{ return CraftResource.Iron; } }

        public BaseArmor( int itemID ) :  base( itemID )
        {
            m_Quality = ArmorQuality.Regular;
            m_Durability = ArmorDurabilityLevel.Regular;
            m_Crafter = null;

            m_Resource = DefaultResource;
            Hue = CraftResources.GetHue( m_Resource );

            m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax( InitMinHits, InitMaxHits );

            this.Layer = (Layer)ItemData.Quality;

            m_AosAttributes = new AosAttributes( this );
            m_AosArmorAttributes = new AosArmorAttributes( this );
            m_AosSkillBonuses = new AosSkillBonuses( this );
	    m_ZuluSkillMods = new ZuluSkillMods( this );
        }

        public override bool AllowSecureTrade( Mobile from, Mobile to, Mobile newOwner, bool accepted )
        {
            if ( !Ethics.Ethic.CheckTrade( from, to, newOwner, this ) )
                return false;

            return base.AllowSecureTrade( from, to, newOwner, accepted );
        }

        public virtual Race RequiredRace { get { return null; } }

        public override bool CanEquip( Mobile from )
        {
            if( !Ethics.Ethic.CheckEquip( from, this ) )
                return false;

            if( from.AccessLevel < AccessLevel.GameMaster )
            {
                if( RequiredRace != null && from.Race != RequiredRace )
                {
                    if( RequiredRace == Race.Elf )
                        from.SendLocalizedMessage( 1072203 ); // Only Elves may use this.
                    else
                        from.SendMessage( "Only {0} may use this.", RequiredRace.PluralName );

                    return false;
                }
                else if( !AllowMaleWearer && !from.Female )
                {
                    if( AllowFemaleWearer )
                        from.SendLocalizedMessage( 1010388 ); // Only females can wear this.
                    else
                        from.SendMessage( "You may not wear this." );

                    return false;
                }
                else if( !AllowFemaleWearer && from.Female )
                {
                    if( AllowMaleWearer )
                        from.SendLocalizedMessage( 1063343 ); // Only males can wear this.
                    else
                        from.SendMessage( "You may not wear this." );

                    return false;
                }
                else
                {
                    int strBonus = ComputeStatBonus( StatType.Str ), strReq = ComputeStatReq( StatType.Str );
                    int dexBonus = ComputeStatBonus( StatType.Dex ), dexReq = ComputeStatReq( StatType.Dex );
                    int intBonus = ComputeStatBonus( StatType.Int ), intReq = ComputeStatReq( StatType.Int );

                    if( from.Dex < dexReq || (from.Dex + dexBonus) < 1 )
                    {
                        from.SendLocalizedMessage( 502077 ); // You do not have enough dexterity to equip this item.
                        return false;
                    }
                    else if( from.Str < strReq || (from.Str + strBonus) < 1 )
                    {
                        from.SendLocalizedMessage( 500213 ); // You are not strong enough to equip that.
                        return false;
                    }
                    else if( from.Int < intReq || (from.Int + intBonus) < 1 )
                    {
                        from.SendMessage( "You are not smart enough to equip that." );
                        return false;
                    }
                }
            }

            return base.CanEquip( from );
        }

        public override bool CheckPropertyConfliction( Mobile m )
        {
            if ( base.CheckPropertyConfliction( m ) )
                return true;

            if ( Layer == Layer.Pants )
                return ( m.FindItemOnLayer( Layer.InnerLegs ) != null );

            if ( Layer == Layer.Shirt )
                return ( m.FindItemOnLayer( Layer.InnerTorso ) != null );

            return false;
        }

        public override bool OnEquip( Mobile from )
        {
            from.CheckStatTimers();

            int strBonus = ComputeStatBonus( StatType.Str );
            int dexBonus = ComputeStatBonus( StatType.Dex );
            int intBonus = ComputeStatBonus( StatType.Int );

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

            return base.OnEquip( from );
        }

        public override void OnRemoved(IEntity parent)
        {
            if ( parent is Mobile )
            {
                Mobile m = (Mobile)parent;
                string modName = this.Serial.ToString();

                m.RemoveStatMod( modName + "Str" );
                m.RemoveStatMod( modName + "Dex" );
                m.RemoveStatMod( modName + "Int" );

                if ( Core.AOS )
                    m_AosSkillBonuses.Remove();

                ((Mobile)parent).Delta( MobileDelta.Armor ); // Tell them armor rating has changed
                m.CheckStatTimers();
            }

            base.OnRemoved( parent );
        }

        public virtual int OnHit( BaseWeapon weapon, int damageTaken )
        {
            double HalfAr = ArmorRating / 2.0;
            int Absorbed = (int)(HalfAr + HalfAr*Utility.RandomDouble());

            damageTaken -= Absorbed;
            if ( damageTaken < 0 )
                damageTaken = 0;

            if ( Absorbed < 2 )
                Absorbed = 2;

            if ( 25 > Utility.Random( 100 ) ) // 25% chance to lower durability
            {
                if ( Core.AOS && m_AosArmorAttributes.SelfRepair > Utility.Random( 10 ) )
                {
                    HitPoints += 2;
                }
                else
                {
                    int wear;

                    if ( weapon.Type == WeaponType.Bashing )
                        wear = Absorbed / 2;
                    else
                        wear = Utility.Random( 2 );

                    if ( wear > 0 && m_MaxHitPoints > 0 )
                    {
                        if ( m_HitPoints >= wear )
                        {
                            HitPoints -= wear;
                            wear = 0;
                        }
                        else
                        {
                            wear -= HitPoints;
                            HitPoints = 0;
                        }

                        if ( wear > 0 )
                        {
                            if ( m_MaxHitPoints > wear )
                            {
                                MaxHitPoints -= wear;

                                if ( Parent is Mobile )
                                    ((Mobile)Parent).LocalOverheadMessage( MessageType.Regular, 0x3B2, 1061121 ); // Your equipment is severely damaged.
                            }
                            else
                            {
                                Delete();
                            }
                        }
                    }
                }
            }

            return damageTaken;
        }

        private string GetNameString()
        {
            string name = this.Name;

            if ( name == null )
                name = String.Format( "#{0}", LabelNumber );

            return name;
        }

        [Hue, CommandProperty( AccessLevel.GameMaster )]
        public override int Hue
        {
            get{ return base.Hue; }
            set{ base.Hue = value; InvalidateProperties(); }
        }

        public override void AddNameProperty( ObjectPropertyList list )
        {
            int oreType;

            switch ( m_Resource )
            {
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
                case CraftResource.SpinedLeather:	oreType = 1061118; break; // spined
                case CraftResource.HornedLeather:	oreType = 1061117; break; // horned
                case CraftResource.BarbedLeather:	oreType = 1061116; break; // barbed
		case CraftResource.RatLeather: oreType = 1160400; break; //Rat
		case CraftResource.WolfLeather: oreType = 1160401; break; //Wolf
		case CraftResource.BearLeather: oreType = 1160402; break; //Bear
		case CraftResource.SerpentLeather: oreType = 1160403; break; //Serpent
		case CraftResource.LizardLeather: oreType = 1160404; break; //Lizard
		case CraftResource.TrollLeather: oreType = 1160405; break; //Troll
		case CraftResource.OstardLeather: oreType = 1160406; break; //Ostard
		case CraftResource.NecromancerLeather: oreType = 1160407; break; //Necromancer
		case CraftResource.LavaLeather: oreType = 1160408; break; //Lava
		case CraftResource.LicheLeather: oreType = 1160409; break; //Liche
		case CraftResource.IceCrystalLeather: oreType = 1160410; break; //IceCrystal
		case CraftResource.DragonLeather: oreType = 1160411; break; //Dragon
		case CraftResource.WyrmLeather: oreType = 1160412; break; //Wyrm
		case CraftResource.BalronLeather: oreType = 1160413; break; //Balron
		case CraftResource.GoldenDragonLeather: oreType = 1160414; break; //GoldenDragon
		case CraftResource.RedScales:		oreType = 1060814; break; // red
                case CraftResource.YellowScales:	oreType = 1060818; break; // yellow
                case CraftResource.BlackScales:		oreType = 1060820; break; // black
                case CraftResource.GreenScales:		oreType = 1060819; break; // green
                case CraftResource.WhiteScales:		oreType = 1060821; break; // white
                case CraftResource.BlueScales:		oreType = 1060815; break; // blue
                default: oreType = 0; break;
            }

            if ( m_Quality == ArmorQuality.Exceptional )
            {
                if ( oreType != 0 )
                    list.Add( 1053100, "#{0}\t{1}", oreType, GetNameString() ); // exceptional ~1_oretype~ ~2_armortype~
                else
                    list.Add( 1050040, GetNameString() ); // exceptional ~1_ITEMNAME~
            }
            else
            {
                if ( oreType != 0 )
                    list.Add( 1053099, "#{0}\t{1}", oreType, GetNameString() ); // ~1_oretype~ ~2_armortype~
                else if ( Name == null )
                    list.Add( LabelNumber );
                else
                    list.Add( Name );
            }
        }

        public override bool AllowEquipedCast( Mobile from )
        {
            if ( base.AllowEquipedCast( from ) )
                return true;

            return ( m_AosAttributes.SpellChanneling != 0 );
        }

        public virtual int GetLuckBonus()
        {
            CraftResourceInfo resInfo = CraftResources.GetInfo( m_Resource );

            if ( resInfo == null )
                return 0;

            CraftAttributeInfo attrInfo = resInfo.AttributeInfo;

            if ( attrInfo == null )
                return 0;

            return attrInfo.ArmorLuck;
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );

            if ( m_Crafter != null )
                list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~

#region Factions
            if ( m_FactionState != null )
                list.Add( 1041350 ); // faction item
#endregion

            if( RequiredRace == Race.Elf )
                list.Add( 1075086 ); // Elves Only

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

            if ( (prop = GetLowerStatReq()) != 0 )
                list.Add( 1060435, prop.ToString() ); // lower requirements ~1_val~%

            if ( (prop = (GetLuckBonus() + m_AosAttributes.Luck)) != 0 )
                list.Add( 1060436, prop.ToString() ); // luck ~1_val~

            if ( (prop = m_AosArmorAttributes.MageArmor) != 0 )
                list.Add( 1060437 ); // mage armor

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

            if ( (prop = m_AosArmorAttributes.SelfRepair) != 0 )
                list.Add( 1060450, prop.ToString() ); // self repair ~1_val~

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

            if ( (prop = GetDurabilityBonus()) > 0 )
                list.Add( 1060410, prop.ToString() ); // durability ~1_val~%

            if ( (prop = ComputeStatReq( StatType.Str )) > 0 )
                list.Add( 1061170, prop.ToString() ); // strength requirement ~1_val~

            if ( m_HitPoints >= 0 && m_MaxHitPoints > 0 )
                list.Add( 1060639, "{0}\t{1}", m_HitPoints, m_MaxHitPoints ); // durability ~1_val~ / ~2_val~
        }

        public override void OnSingleClick( Mobile from )
        {
            if( this.Name == null ){
		String prefix = "";
		String suffix  = "";
		
		if( m_Identified || from.AccessLevel >= AccessLevel.GameMaster ){
		    if( m_Quality == ArmorQuality.Exceptional ){
			prefix += "Exceptional ";
		    }

		    if( m_Resource != CraftResource.Iron &&
			m_Resource != CraftResource.RegularLeather &&
			m_Resource != CraftResource.RegularWood &&
			m_Resource != CraftResource.None) {

			prefix += CraftResources.GetName( m_Resource ) + " ";
		    }

		    switch( m_Durability ){
			case ArmorDurabilityLevel.Durable:
			    {
				prefix += "Durable ";
				break;
			    }
			case ArmorDurabilityLevel.Substantial:
			    {
				prefix += "Substantial ";
				break;
			    }
			case ArmorDurabilityLevel.Massive:
			    {
				prefix += "Massive ";
				break;
			    }
			case ArmorDurabilityLevel.Fortified:
			    {
				prefix += "Fortified ";
				break;
			    }
			case ArmorDurabilityLevel.Indestructible:
			    {
				prefix += "Indestructible ";
				break;
			    }
			default:
			    break;
		    }

		    switch( m_Protection ){
			case ArmorProtectionLevel.Defense:
			    {
				suffix += " of Defense";
				break;
			    }
			case ArmorProtectionLevel.Guarding:
			    {
				suffix += "of Guarding";
				break;
			    }
			case ArmorProtectionLevel.Hardening:
			    {
				suffix += "of Hardening";
				break;
			    }
			case ArmorProtectionLevel.Fortification:
			    {
				suffix += "of Fortification";
				break;
			    }
			case ArmorProtectionLevel.Invulnerability:
			    {
				suffix += "of Invulnerability";
				break;
			    }
			default:
			    break;
		    }
		}
		else {
		    //not identified or not staff
		    prefix = "unidentified ";
		}

		LabelToAffix(from, LabelNumber, AffixType.Prepend, prefix);
	    }
	    else {
		base.OnSingleClick( from );
	    }
		/*
            if ( DisplayLootType )
            {
                if ( LootType == LootType.Blessed )
                    attrs.Add( new EquipInfoAttribute( 1038021 ) ); // blessed
                else if ( LootType == LootType.Cursed )
                    attrs.Add( new EquipInfoAttribute( 1049643 ) ); // cursed
            }

#region Factions
            if ( m_FactionState != null )
                attrs.Add( new EquipInfoAttribute( 1041350 ) ); // faction item
#endregion

            if ( m_Quality == ArmorQuality.Exceptional )
                attrs.Add( new EquipInfoAttribute( 1018305 - (int)m_Quality ) );

            //if ( m_Resource != CraftResource.Iron )
            //    attrs.Add( new EquipInfoAttribute( 1160200 + (int)m_Resource - 2 ) );  //source of our bug?

	    
		if( res < 100 ) {
		    //ore, see ResourceInfo.cs
		    attrs.Add( new EquipInfoAttribute( 1160200 + res - 2) );
		}
		else if( 100 < res && res <= 104 ){
		    //leather, OSI
		    attrs.Add( new EquipInfoAttribute( 1061116 + res - 102 ) );
		}
		else if( 104 < res && res <= 200){
		    //leather, zulu
		    attrs.Add( new EquipInfoAttribute( 1160400 + res - 105 ) );
		}
		else if( 301 < res ) {
		    //wood
		    attrs.Add( new EquipInfoAttribute( 1160233 + res - 300) ); //yuck
		}
	    }	    

            if ( m_Identified || from.AccessLevel >= AccessLevel.GameMaster)
            {
                if ( m_Durability != ArmorDurabilityLevel.Regular )
                    attrs.Add( new EquipInfoAttribute( 1038000 + (int)m_Durability ) );

                if ( m_Protection > ArmorProtectionLevel.Regular && m_Protection <= ArmorProtectionLevel.Invulnerability )
                    attrs.Add( new EquipInfoAttribute( 1038005 + (int)m_Protection ) );
            }
            else if ( m_Durability != ArmorDurabilityLevel.Regular || (m_Protection > ArmorProtectionLevel.Regular && m_Protection <= ArmorProtectionLevel.Invulnerability) )
                attrs.Add( new EquipInfoAttribute( 1038000 ) ); // Unidentified


            int number;

            if ( Name == null )
            {
                number = LabelNumber;
            }
            else
            {
                this.LabelTo( from, Name );
                number = 1041000;
            }

            if ( attrs.Count == 0 && Crafter == null && Name != null )
                return;

            EquipmentInfo eqInfo = new EquipmentInfo( number, m_Crafter, false, attrs.ToArray() );

            from.Send( new DisplayEquipmentInfo( this, eqInfo ) );
		*/
        }

#region ICraftable Members

        public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
        {
            Quality = (ArmorQuality)quality;

            if ( makersMark )
                Crafter = from;

            //resource type appears to set the hue
            //someone already added a partial switch statement for AR based on material up towards the top of the class so I just continued in that vein --sith
            Type resourceType = typeRes;

            if ( resourceType == null )
                resourceType = craftItem.Resources.GetAt( 0 ).ItemType;

            Resource = CraftResources.GetFromType( resourceType );
            PlayerConstructed = true;

            CraftContext context = craftSystem.GetContext( from );

            if ( context != null && context.DoNotColor ){
                Hue = 0;
	    }

            if( Quality == ArmorQuality.Exceptional )
            {
                if ( !( Core.ML && this is BaseShield ))		// Guessed Core.ML removed exceptional resist bonuses from crafted shields
                    DistributeBonuses( (tool is BaseRunicTool ? 6 : Core.SE ? 15 : 14) ); // Not sure since when, but right now 15 points are added, not 14.

                if( Core.ML && !(this is BaseShield) )
                {
                    int bonus = (int)(from.Skills.ArmsLore.Value / 20);

                    for( int i = 0; i < bonus; i++ )
                    {
                        switch( Utility.Random( 5 ) )
                        {
                            case 0: m_PhysicalBonus++;	break;
                            case 1: m_FireBonus++;		break;
                            case 2: m_ColdBonus++;		break;
                            case 3: m_EnergyBonus++;	break;
                            case 4: m_PoisonBonus++;	break;
                        }
                    }

                    from.CheckSkill( SkillName.ArmsLore, 0, 130 );
                }
            }

            if ( Core.AOS && tool is BaseRunicTool )
                ((BaseRunicTool)tool).ApplyAttributesTo( this );

            return quality;
        }

#endregion
    }
}
