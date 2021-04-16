using System;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class DetectHidden : BaseSkillHandler
    {
        public override SkillName Skill => SkillName.DetectHidden;

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            if (!from.ShilCheckSkill(Skill))
            {
                from.SendFailureMessage(500817); // You can see nothing hidden there.
                return Delay;
            }

            var range = (int)(from.Skills.DetectHidden.Value / 15.0 * from.GetClassModifier(Skill));

            var eable = from.GetMobilesInRange(range);
            var hiddenMobiles = eable.Where(mobile => TryDetect(from, mobile)).ToList();
            eable.Free();

            hiddenMobiles.ForEach(mobile =>
            {
                mobile.Hidden = false;
                mobile.SendFailureLocalOverHeadMessage(500814); // You have been revealed!
                from.SendSuccessPrivateOverHeadMessage(mobile, "You found someone!");
            });

            var itemEable = from.GetItemsInRange(range);
            foreach (var item in itemEable)
            {
                if (item is BaseTrap || item is TrapableContainer container && container.TrapType != TrapType.None)
                {
                    item.OnSingleClick(from);
                    item.LabelTo(from, 500851); // [trapped]
                }
            }

            var found = hiddenMobiles.Count + itemEable.Count();
            
            if(found == 0)
                from.SendFailureMessage(500817); // You can see nothing hidden there.
            
            itemEable.Free();

            return Delay;
        }

        private static bool TryDetect(Mobile finder, Mobile hider)
        {
            if (!finder.Hidden || finder == hider)
                return false;

            var finderSkill = finder.Skills.DetectHidden.Value * finder.GetClassModifier(SkillName.DetectHidden);
            var hiderSkill = hider.Skills.DetectHidden.Value * hider.GetClassModifier(SkillName.DetectHidden);

            var chance = finderSkill - (hiderSkill / 2);

            return Utility.RandomMinMax(1, 100) < chance;
        }
    }
}