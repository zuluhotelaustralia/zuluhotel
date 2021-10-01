using System;
using System.Threading.Tasks;
using Server;

namespace ZuluContent.Zulu.Skills
{
    public abstract class BaseSkillHandler
    {
        public abstract SkillName Skill { get; }
        public static TimeSpan Delay => ZhConfig.Skills.Entries[SkillName.Anatomy].Delay;

        public BaseSkillHandler()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            SkillInfo.Table[(int)Skill].Callback = mobile => DispatchOnUseSkillHandler(mobile, this);
        }

        public abstract Task<TimeSpan> OnUse(Mobile @from);

        public static TimeSpan DispatchOnUseSkillHandler(Mobile mobile, BaseSkillHandler handler)
        {
            handler.OnUse(mobile)
                .ContinueWith(t => mobile.NextSkillTime = Core.TickCount + (int) t.Result.TotalMilliseconds);
            
            // Prevent skill use whilst async handler is running
            return TimeSpan.FromHours(1);
        }
    }
}