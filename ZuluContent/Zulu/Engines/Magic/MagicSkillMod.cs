using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Engines.Magic
{
    public class MagicSkillMod : SkillMod, IMagicMod<SkillName>
    {
        public MagicProp Prop { get; } = MagicProp.Skill;
        public MagicInfo Info => MagicInfo.MagicInfoMap[Target];
        public string EnchantName => Info.GetName(Value > 5 ? (int)Value / 5 : (int)Value, Cursed);
        public bool Cursed { get; set; }
        public SkillName Target => Skill;
        
        public MagicSkillMod(SkillName skill, double value) : base(skill, true, value)
        {
        }

        public void AddTo(Mobile mobile)
        {
            mobile.AddSkillMod(this);
        }

        public override bool CheckCondition() => true;
    }
}