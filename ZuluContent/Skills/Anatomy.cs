using System;
using Scripts.Zulu.Engines.Classes;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using static Scripts.Zulu.Engines.Classes.SkillCheck;

namespace Server.SkillHandlers
{
    public class Anatomy
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int) SkillName.Anatomy].Callback = OnUse;
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.Target = new InternalTarget();

            m.SendLocalizedMessage(500321); // Whom shall I examine?

            return Configs[SkillName.Anatomy].Delay;
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(-1, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!(targeted is Mobile))
                {
                    from.SendAsciiMessage(33, "This has no anatomy at all!");
                    return;
                }

                if ((from as IShilCheckSkill)?.CheckSkill(SkillName.Anatomy, -1,
                    Configs[SkillName.Anatomy].DefaultPoints) == false)
                {
                    from.SendAsciiMessage("You're not sure...");
                    return;
                }

                if (targeted is Mobile mobile)
                {
                    var strength = mobile.Str;
                    var dexterity = mobile.Dex;

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

                    from.SendAsciiMessage(55, strMessage);
                    from.SendAsciiMessage(55, dexMessage);

                    if (from.Skills[SkillName.Anatomy].Value > 75)
                    {
                        var percent = mobile.Stam * 100 / Math.Max(mobile.StamMax, 1);
                        from.SendAsciiMessage(55, $"This being is at {percent}% of their max vigor.");
                    }
                }
            }
        }
    }
}