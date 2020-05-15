using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Regions
{
    public class WildernessRegion : BaseRegion
    {
        //this is intended to demarcate areas where ranger skills should train faster than normal

        public override double GetSkillSpecificFactor(Skill skill)
        {
            base.GetSkillSpecificFactor(skill);

            switch (skill.SkillName)
            {

                case SkillName.Tracking:
                case SkillName.AnimalTaming:
                case SkillName.Camping:
                case SkillName.Herding:
                case SkillName.Fishing:
                case SkillName.Lumberjacking:
                case SkillName.Mining:
                    return 0.5;
                default:
                    return RegionalSkillGainPrimaryFactor;
            }
        }

        public WildernessRegion(XmlElement xml, Map map, Region parent) : base(xml, map, parent)
        {
        }
    }
}
