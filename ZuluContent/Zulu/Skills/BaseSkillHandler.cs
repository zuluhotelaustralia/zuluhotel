using System;
using System.Threading.Tasks;
using Server;

namespace ZuluContent.Zulu.Skills
{
    public abstract class BaseSkillHandler
    {
        public abstract SkillName Skill { get; }
        public static TimeSpan Delay => ZhConfig.Skills.Entries[SkillName.Anatomy].Delay;
        
        
        public abstract Task<TimeSpan> OnUse(Mobile mobile);

        public static TimeSpan DispatchOnUseSkillHandler(Mobile mobile, BaseSkillHandler handler)
        {
            handler.OnUse(mobile)
                .ContinueWith(t => mobile.NextSkillTime = Core.TickCount + (int) t.Result.TotalMilliseconds);
            
            return TimeSpan.MaxValue;
        }
    }
}