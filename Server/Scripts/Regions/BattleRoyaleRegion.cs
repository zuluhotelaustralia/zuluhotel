using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

using Server;

namespace Server.Regions {
    public class BattleRoyaleRegion : TownRegion {
	public BattleRoyaleRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent ) {}

	public override double RegionalSkillGainPrimaryFactor { get { return 0.03; } } //about 6 hours to cap it at 1 attempt per second
	public override double RegionalSkillGainSecondaryFactor { get { return 1600.0; } }

	public override double GetSkillSpecificFactor( Skill skill ){
	    // note to self: fallthrough is legal in c# iff you don't do any processing in the case
	    switch( skill.SkillName ){
		case SkillName.Magery:
		    return 0.2;
		case SkillName.Healing:
		case SkillName.Veterinary:
		    return 0.3;
		case SkillName.AnimalTaming:
		    return 0.2;
		case SkillName.Meditation:
    		case SkillName.Archery:
		case SkillName.Fencing:
		case SkillName.Swords:
		case SkillName.Parry:
		case SkillName.Macing:
		case SkillName.Tracking:
		case SkillName.Wrestling:
		case SkillName.Tactics:
		    return 0.1;
		case SkillName.RemoveTrap:
		case SkillName.Musicianship:
		    return 0.9;
		case SkillName.DetectHidden:
		    return 0.1;
		case SkillName.MagicResist:
		    return 0.2;
		default:
		    return RegionalSkillGainPrimaryFactor;
	    }
	}

	
	public override TimeSpan GetLogoutDelay( Mobile m ) {
	    List<Mobile> players = Server.BattleRoyale.GameController.PlayerList;

	    if( players.Contains( m ) ){
		return Server.BattleRoyale.GameController.LogoutDelay;
	    }

	    return base.GetLogoutDelay( m );
	}
    }
}
