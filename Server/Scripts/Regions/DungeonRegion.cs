using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Regions
{
    public class DungeonRegion : BaseRegion
    {
	public override bool YoungProtected { get { return false; } }
	
	public override double RegionalSkillGainPrimaryFactor { get { return 0.03; } } //about 6 hours to cap it at 1 attempt per second
	public override double RegionalSkillGainSecondaryFactor { get { return 1600.0; } }

	public override double GetSkillSpecificFactor( Skill skill ){
	    switch( skill.SkillName ){
		case SkillName.Magery:
		    return 0.2;
		case SkillName.Meditation:
		    return 0.1;
		case SkillName.Healing:
		    return 0.3;
		case SkillName.Veterinary:
		    return 0.3;
		case SkillName.AnimalTaming:
		    return 0.2;
		case SkillName.Archery:
		    return 0.1;
		case SkillName.Fencing:
		    return 0.1;
		case SkillName.Swords:
		    return 0.1;
		case SkillName.Parry:
		    return 0.1;
		case SkillName.Macing:
		    return 0.1;
		case SkillName.Tracking:
		    return 0.1;
		case SkillName.Wrestling:
		    return 0.1;
		case SkillName.Tactics:
		    return 0.1;
		case SkillName.RemoveTrap:
		    return 0.9;
		case SkillName.DetectHidden:
		    return 0.1;
		case SkillName.MagicResist:
		    return 0.2;
		default:
		    return RegionalSkillGainPrimaryFactor;
	    }
	}
	
	private Point3D m_EntranceLocation;
	private Map m_EntranceMap;

	public Point3D EntranceLocation{ get{ return m_EntranceLocation; } set{ m_EntranceLocation = value; } }
	public Map EntranceMap{ get{ return m_EntranceMap; } set{ m_EntranceMap = value; } }

	public DungeonRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
	{
	    XmlElement entrEl = xml["entrance"];

	    Map entrMap = map;
	    ReadMap( entrEl, "map", ref entrMap, false );

	    if ( ReadPoint3D( entrEl, entrMap, ref m_EntranceLocation, false ) )
		m_EntranceMap = entrMap;
	}

	public override bool AllowHousing( Mobile from, Point3D p )
	{
	    return false;
	}

	public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
	{
	    global = LightCycle.DungeonLevel;
	}

	public override bool CanUseStuckMenu( Mobile m )
	{
	    if ( this.Map == Map.Felucca )
		return false;

	    return base.CanUseStuckMenu( m );
	}
    }
}
