using System;
using Scripts.Zulu.Utilities;
using Server.Targeting;
using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.SkillHandlers
{
    public class ItemId
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int) SkillName.ItemID].Callback = OnUse;
        }

        public static TimeSpan OnUse(Mobile from)
        {
            from.SendLocalizedMessage(500343); // What do you wish to appraise and identify?
            from.Target = new InternalTarget();

            return TimeSpan.FromSeconds(1.0);
        }

        [PlayerVendorTarget]
        private class InternalTarget : Target
        {
            public InternalTarget() : base(8, false, TargetFlags.None)
            {
                AllowNonlocal = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                switch (o)
                {
                    case IMagicItem item when from.CheckTargetSkill(SkillName.ItemID, item, 0, 100):
                    {
                        if (!item.Identified)
                            item.Identified = true;
                        item.OnSingleClick(from);
                        from.SendSuccessMessage("It appears to be " + GetMagicItemName(item));
                        break;
                    }
                    case Item item:
                        from.SendLocalizedMessage(500353); // You are not certain...
                        break;
                    case Mobile mobile:
                        mobile.OnSingleClick(from);
                        break;
                    default:
                        from.SendLocalizedMessage(500353); // You are not certain...
                        break;
                }
            }
        }
    }
}