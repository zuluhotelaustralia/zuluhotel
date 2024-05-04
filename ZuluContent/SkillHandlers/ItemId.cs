using System;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Skills;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.SkillHandlers
{
    public class ItemId : BaseSkillHandler
    {
        public override SkillName Skill => SkillName.ItemID;

        private static readonly TargetOptions TargetOptions = new()
        {
            Range = 2,
        };

        private static TimeSpan GetDelay(Mobile from)
        {
            if (from is IZuluClassed { ZuluClass: { Type: ZuluClassType.Mage, Level: >= 5 } })
                return TimeSpan.Zero;

            return Delay;
        }
        
        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            var target = new AsyncTarget<object>(from, TargetOptions);
            from.Target = target;
            
            from.SendSuccessMessage(500343); // What do you wish to appraise and identify?
            
            var (targeted, responseType) = await target;

            if (responseType != TargetResponseType.Success)
                return GetDelay(from);

            switch (targeted)
            {
                case IMagicItem magicItem when from.ShilCheckSkill(SkillName.ItemID):
                {
                    if (!magicItem.Identified)
                    {
                        magicItem.Identified = true;
                        // Update the color of the item
                        if (magicItem is Item item)
                            item.Delta(ItemDelta.Update);
                    }
                    magicItem.OnSingleClick(from);
                    from.SendSuccessMessage($"It appears to be {GetMagicItemName(magicItem)}");
                    break;
                }
                case Item:
                    from.SendFailureMessage(500353); // You are not certain...
                    break;
                case Mobile mobile:
                    mobile.OnSingleClick(from);
                    break;
                default:
                    from.SendFailureMessage(500353); // You are not certain...
                    break;
            }

            return GetDelay(from);
        }
    }
}