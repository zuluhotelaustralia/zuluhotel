using System;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Spells;
using ZuluContent.Multis;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class Hiding : BaseSkillHandler
    {
        public override SkillName Skill => SkillName.Hiding;

        public override async Task<TimeSpan> OnUse(Mobile mobile)
        {
            if (mobile.Spell != null || mobile.Target != null)
            {
                mobile.SendLocalizedMessage(501238); // You are busy doing something else and cannot hide.
                return TimeSpan.FromSeconds(1.0);
            }

            if (mobile.Warmode)
            {
                mobile.SendFailureLocalOverHeadMessage("You cannot hide while in War Mode.");
                return Delay;
            }

            var range = (int) (mobile.Skills[Skill].Value switch
            {
                > 130 => 1,
                > 110 => 2,
                > 90 => 3,
                > 75 => 4,
                > 60 => 5,
                > 45 => 6,
                > 30 => 8,
                > 15 => 9,
                _ => 10
            } / mobile.GetClassModifier(Skill));

            var eable = mobile.GetMobilesInRange(range);
            var hostiles = eable.Any(m => m.Combatant == mobile);
            eable.Free();

            if (hostiles)
            {
                mobile.SendFailureLocalOverHeadMessage("You cannot hide as there are hostiles in view!");
                return Delay;
            }

            if (mobile.GetMulti()?.IsMultiFriend(mobile) == true || mobile.ShilCheckSkill(Skill))
            {
                mobile.SendSuccessLocalOverHeadMessage(501240); // You have hidden yourself well.
                mobile.TryAddBuff(new Invisibility());
                return Delay;
            }

            mobile.SendFailureLocalOverHeadMessage(501237); // You can't seem to hide right now.

            return Delay;
        }
    }
}