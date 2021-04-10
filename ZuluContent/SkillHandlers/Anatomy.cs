using System;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Targeting;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class Anatomy : BaseSkillHandler
    {
        public override SkillName Skill { get; } = SkillName.Anatomy;

        private static readonly TargetOptions TargetOptions = new()
        {
            Range = 12,
        };

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            var target = new AsyncTarget<Mobile>(from, TargetOptions);
            from.Target = target;
            
            from.SendLocalizedMessage(500321); // Whom shall I examine?
            
            var (targeted, responseType) = await target;

            if (responseType != TargetResponseType.Success)
                return Delay;

            if (targeted == null)
            {
                from.SendFailureMessage("This has no anatomy at all!");
                return Delay;
            }
            
            if (!from.ShilCheckSkill(SkillName.Anatomy))
            {
                from.SendAsciiMessage("You're not sure...");
                return Delay;
            }

            var strength = targeted.Str;
            var dexterity = targeted.Dex;

            var strMessage = strength switch
            {
                > 150 => "Superhumanly strong.",
                > 135 => "One of the strongest people you have ever seen.",
                > 120 => "Strong as an ox.",
                > 105 => "Extraordinarily strong.",
                > 90 => "Extremely strong.",
                > 75 => "Very strong.",
                > 60 => "Somewhat strong.",
                > 45 => "To be of normal strength.",
                > 30 => "Somewhat weak.",
                > 15 => "Rather Feeble.",
                _ => "Like they would have trouble lifting small objects."
            };

            var dexMessage = dexterity switch
            {
                > 150 => "Superhumanly agile.",
                > 135 => "One of the fastest people you have ever seen.",
                > 120 => "Moves like quicksilver.",
                > 105 => "Extraordinarily agile.",
                > 90 => "Extremely agile.",
                > 75 => "Very agile.",
                > 60 => "Somewhat agile.",
                > 45 => "Moderately dextrous.",
                > 30 => "Somewhat uncoordinated.",
                > 15 => "Very clumsy.",
                _ => "Like they barely manage to stay standing."
            };

            from.SendSuccessMessage(strMessage);
            from.SendSuccessMessage(dexMessage);

            if (from.Skills[SkillName.Anatomy].Value > 75)
            {
                var percent = targeted.Stam * 100 / Math.Max(targeted.StamMax, 1);
                from.SendSuccessMessage($"This being is at {percent}% of their max vigor.");
            }

            return ZhConfig.Skills.Entries[SkillName.Anatomy].Delay;
        }
    }
}