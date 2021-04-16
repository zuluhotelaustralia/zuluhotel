using System;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Network;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    class Meditation : BaseSkillHandler
    {
        public override SkillName Skill { get; } = SkillName.Meditation;

        public override async Task<TimeSpan> OnUse(Mobile mobile)
        {
            mobile.RevealingAction();

            if (mobile.Mana >= mobile.ManaMax)
            {
                mobile.SendSuccessMessage(501846); // You are at peace.
                return Delay;
            }

            if (mobile.Poisoned)
            {
                mobile.SendFailureMessage("You can't meditate while poisoned.");
                return Delay;
            }

            if (mobile.Warmode)
            {
                mobile.SendFailureMessage("You can't meditate in war mode.");
                return Delay;
            }

            if (!SpellHelper.CheckValidHands(mobile))
            {
                mobile.SendFailureMessage(502626); // Your hands must be free to cast spells or meditate.
                return Delay;
            }
            
            if (GetMagicEfficiencyModifier(mobile) <= 0)
            {
                mobile.SendFailureMessage("Regenerative forces cannot penetrate your armor.");
                return Delay;
            }
            
            if (!mobile.ShilCheckSkill(SkillName.Meditation))
            {
                mobile.SendFailureMessage("You cannot focus your concentration.");
                return Delay;
            }
            
            mobile.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "*Meditating*");
            mobile.SendSuccessMessage(501851); // You enter a meditative trance.

            mobile.PlaySound(0xF9);
                
            var regenBase = (int) (mobile.Skills[SkillName.Meditation].Value / 25 + mobile.Int / 35.0);
            var interval = 5.0;

            mobile.FireHook(h => h.OnMeditation(mobile, ref regenBase, ref interval));
            var intervalTimespan = TimeSpan.FromSeconds(interval);

            var shouldBreakConcentration = GetShouldBreakConcentration(mobile);

            mobile.Meditating = true;
            while (mobile.Mana < mobile.ManaMax && mobile.Meditating && !shouldBreakConcentration())
            {
                await Timer.Pause(intervalTimespan);
                
                if (!mobile.Meditating)
                    break;

                var modifier = GetMagicEfficiencyModifier(mobile);
                
                if (modifier > 0)
                {
                    var restored = (int)(regenBase * modifier / 100);
                    if (restored >= 0)
                    {
                        mobile.Mana += restored;
                    }
                    else
                    {
                        mobile.SendFailureMessage("Regenerative forces cannot penetrate your armor.");
                        break;
                    }
                }
            }
            mobile.Meditating = false;
            mobile.SendLocalizedMessage(500134); // You stop meditating.

            return Delay;
        }

        private static double GetMagicEfficiencyModifier(Mobile from)
        {
            if (from is IZuluClassed classed)
            {
                var penalty = classed.ZuluClass.GetMagicEfficiencyPenalty();
                return 100 - penalty * 2;
            }

            return 0;
        }
        
        private static Func<bool> GetShouldBreakConcentration(Mobile mobile)
        {
            var startHits = mobile.Hits;
            var startLocation = mobile.Location;
            
            return () =>
            {
                if (mobile.Location != startLocation)
                    return true;

                if (mobile.Mana == mobile.ManaMax)
                    return true;

                if (mobile.Poisoned)
                    return true;

                if (mobile.Warmode)
                    return true;

                if (!SpellHelper.CheckValidHands(mobile))
                    return true;

                if (mobile.Hits < startHits)
                    return true;

                return false;
            };
        }
    }
}