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

        public override double RegionalSkillGainPrimaryFactor { get { return base.RegionalSkillGainPrimaryFactor; } } //about 6 hours to cap it at 1 attempt per second
        public override double RegionalSkillGainSecondaryFactor { get { return base.RegionalSkillGainSecondaryFactor; } }

        public override double GetSkillSpecificFactor(Skill skill)
        {
            // note to self: fallthrough is legal in c# iff you don't do any processing in the case
            switch (skill.SkillName)
            {
                case SkillName.Magery:
                    return 0.2;
                case SkillName.Healing:
                case SkillName.Veterinary:
                    return 0.3;
                case SkillName.Meditation:
                case SkillName.Archery:
                case SkillName.Fencing:
                case SkillName.Swords:
                case SkillName.Parry:
                case SkillName.Macing:
                case SkillName.Tracking:
                case SkillName.Wrestling:
                case SkillName.Tactics:
                case SkillName.Hiding:
                case SkillName.Stealth:
                case SkillName.Snooping:
                case SkillName.Stealing:
                    return 0.4;
                case SkillName.RemoveTrap:
                case SkillName.Musicianship:
                case SkillName.ItemID:
                    return 0.5;
                case SkillName.DetectHidden:
                    return 0.2;
                case SkillName.MagicResist:
                    return 0.3;
                default:
                    return base.GetSkillSpecificFactor(skill);
            }
        }

        private Point3D m_EntranceLocation;
        private Map m_EntranceMap;

        public Point3D EntranceLocation { get { return m_EntranceLocation; } set { m_EntranceLocation = value; } }
        public Map EntranceMap { get { return m_EntranceMap; } set { m_EntranceMap = value; } }

        public DungeonRegion(XmlElement xml, Map map, Region parent) : base(xml, map, parent)
        {
            XmlElement entrEl = xml["entrance"];

            Map entrMap = map;
            ReadMap(entrEl, "map", ref entrMap, false);

            if (ReadPoint3D(entrEl, entrMap, ref m_EntranceLocation, false))
                m_EntranceMap = entrMap;
        }

        public override bool AllowHousing(Mobile from, Point3D p)
        {
            return false;
        }

        public override void AlterLightLevel(Mobile m, ref int global, ref int personal)
        {
            global = LightCycle.DungeonLevel;
        }

        public override bool CanUseStuckMenu(Mobile m)
        {
            if (this.Map == Map.Felucca)
                return false;

            return base.CanUseStuckMenu(m);
        }
    }
}
