using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Regions {
    public class CraftingRegion : GuardedRegion {
	// this is intended to denote areas where crafting skills should gain faster than normal
	// e.g. minoc mines, etc.

	public override double GetSkillSpecificFactor(Skill skill){
	    if( skill.SkillName == SkillName.Alchemy ||
		skill.SkillName == SkillName.Blacksmith ||
		skill.SkillName == SkillName.Fletching ||
		//skill.SkillName == SkillName.Mining ||
		skill.SkillName == SkillName.Carpentry ||
		skill.SkillName == SkillName.Tailoring ||
		skill.SkillName == SkillName.Tinkering ||
		//skill.SkillName == SkillName.Lumberjacking ||
		skill.SkillName == SkillName.ArmsLore ||
		skill.SkillName == SkillName.ItemID ||
		skill.SkillName == SkillName.Inscribe ||
		skill.SkillName == SkillName.Cooking) {
		return 0.1;
	    }
	    else {
		return RegionalSkillGainPrimaryFactor;
	    }
	}

	public CraftingRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
	{}
    }
}
