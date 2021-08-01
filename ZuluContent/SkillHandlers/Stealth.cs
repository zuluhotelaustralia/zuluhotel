using System;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class Stealth : BaseSkillHandler
    {
        public override SkillName Skill => SkillName.Stealth;

        public static double HidingRequirement => 80;

        public override async Task<TimeSpan> OnUse(Mobile mobile)
        {
            if (!mobile.Hidden)
            {
                mobile.SendFailureMessage(502725); // You must hide first
                return Delay;
            }

            if (mobile.Skills[SkillName.Hiding].Value < HidingRequirement)
            {
                mobile.SendFailureMessage(502726); // You are not hidden well enough.  Become better at hiding.
                mobile.RevealingAction();
                return Delay;
            }

            if (!mobile.CanBeginAction(typeof(Stealth)))
            {
                mobile.SendFailureMessage(1063086); // You cannot use this skill right now.
                mobile.RevealingAction();
                return Delay;
            }

            if (!mobile.ShilCheckSkill(SkillName.Stealth))
            {
                mobile.SendFailureLocalOverHeadMessage(502731); // You fail in your attempt to move unnoticed.
                mobile.RevealingAction();
                return Delay;
            }

            var steps = mobile.Skills[SkillName.Stealth].Value / 12.0 + 1;

            steps *= mobile.GetClassModifier(Skill);

            mobile.AllowedStealthSteps = (int) steps;
            mobile.SendSuccessMessage(502730); // You begin to move quietly.

            Timer.DelayCall(Delay, StealthReady_Callback, mobile);

            return Delay;
        }

        public static void StealthReady_Callback(Mobile mobile)
        {
            mobile.SendSuccessMessage("You feel ready to stealth again.");
        }
    }
}