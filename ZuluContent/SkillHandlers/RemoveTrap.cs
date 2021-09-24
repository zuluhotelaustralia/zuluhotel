using System;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Targeting;
using Server.Items;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class RemoveTrap : BaseSkillHandler
    {
        public override SkillName Skill => SkillName.RemoveTrap;

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            from.SendSuccessMessage(502368); // Which trap will you attempt to disarm?

            var target = new AsyncTarget<object>(from,
                new TargetOptions {Range = 1});
            from.Target = target;

            var (targeted, _) = await target;

            if (targeted is Mobile)
            {
                from.SendFailureMessage(502816); // You feel that such an action would be inappropriate
                return Delay;
            }

            if (!(targeted is TrapableContainer trapable))
            {
                from.SendFailureMessage(502373); // That does'nt appear to be trapped
                return Delay;
            }

            from.Direction = from.GetDirectionTo(trapable);

            if (trapable.TrapType == TrapType.None)
            {
                from.SendFailureMessage(502373); // That doesn't appear to be trapped
                return Delay;
            }

            from.PlaySound(0x241);

            if (from.ShilCheckSkill(SkillName.RemoveTrap, trapable.TrapLevel))
            {
                trapable.TrapStrength = 0;
                trapable.TrapLevel = 0;
                trapable.TrapType = TrapType.None;
                from.SendSuccessMessage(502377); // You successfully render the trap harmless
            }
            else
            {
                from.SendFailureMessage(502372); // You fail to disarm the trap... but you don't set it off
            }

            return Delay;
        }
    }
}