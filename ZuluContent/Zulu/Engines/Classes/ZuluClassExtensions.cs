using System.Linq;
using Server;

namespace Scripts.Zulu.Engines.Classes
{
    public static class ZuluClassExtensions
    {
        public static bool ClassContainsSkill(this Mobile mobile, params SkillName[] skills)
        {
            if (mobile is IZuluClassed {ZuluClass: { } cls})
                return skills.All(s => cls.IsSkillInClass(s));

            return false;
        }
        
        public static double GetClassModifier(this Mobile mobile)
        {
            if (mobile is IZuluClassed {ZuluClass: { } cls})
                return cls.Bonus;

            return 1.0;
        }
        
        public static double GetClassModifier(this Mobile mobile, SkillName skill)
        {
            if (mobile is IZuluClassed {ZuluClass: { } cls} && cls.IsSkillInClass(skill))
                return cls.Bonus;

            return 1.0;
        }
    }
}