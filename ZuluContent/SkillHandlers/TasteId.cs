using System;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Targeting;
using Server.Items;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class TasteId : BaseSkillHandler
    {
        public override SkillName Skill => SkillName.TasteID;

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            var target = new AsyncTarget<object>(from,
                new TargetOptions {Range = 2});
            from.Target = target;
            from.RevealingAction();

            from.SendSuccessMessage(502807); // What would you like to taste?

            var (targeted, _) = await target;
            
            if (targeted is Mobile)
            {
                from.SendFailureMessage(502816); // You feel that such an action would be inappropriate.
                return Delay;
            }
            
            if (targeted is Food food)
            {
                if (from.ShilCheckSkill(SkillName.TasteID))
                {
                    if (food.Poison != null)
                    {
                        food.SendLocalizedMessageTo(from, 1038284); // It appears to have poison smeared on it.
                    }
                    else
                    {
                        // No poison on the food
                        food.SendLocalizedMessageTo(from, 1010600); // You detect nothing unusual about this substance.
                    }
                }
                else
                {
                    // Skill check failed
                    food.SendLocalizedMessageTo( from, 502823 ); // You cannot discern anything about this substance.
                }
            }
            else if (targeted is BasePotion potion)
            {
                potion.SendLocalizedMessageTo(from, 502813); // You already know what kind of potion that is.
                potion.SendLocalizedMessageTo(from, potion.LabelNumber);
            }
            else if (targeted is PotionKeg keg)
            {
                if ( keg.Held <= 0 )
                {
                    keg.SendLocalizedMessageTo( from, 502228 ); // There is nothing in the keg to taste!
                }
                else
                {
                    keg.SendLocalizedMessageTo( from, 502229 ); // You are already familiar with this keg's contents.
                    keg.SendLocalizedMessageTo( from, keg.LabelNumber );
                }
            }
            else
            {
                from.SendFailureMessage(502820); // That's not something you can taste.
            }

            return Delay;
        }
    }
}