using System;
using System.Xml;
using Server;

namespace Server.Regions
{
    public class TownRegion : GuardedRegion
    {

        public override double GetSkillSpecificFactor(Skill skill)
        {
            return base.GetSkillSpecificFactor(skill);
        }

        public TownRegion(XmlElement xml, Map map, Region parent) : base(xml, map, parent)
        {
        }

    }
}
