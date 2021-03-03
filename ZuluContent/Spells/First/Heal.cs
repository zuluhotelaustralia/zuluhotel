using System.Threading.Tasks;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
#pragma warning disable 1998

namespace Server.Spells.First
{
    public class HealSpell : MagerySpell
    {
        public HealSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public override async Task OnCastAsync(TargetResponse<object> response = null)
        {
            if (!(response?.Target is Mobile mobile))
                return;

            if (!Caster.CanSee(mobile))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
                return;
            }
            
            if (mobile.Poisoned)
            {
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, Caster == mobile ? 1005000 : 1010398);
                return;
            }
            
            if (!CheckBeneficialSequence(mobile))
                return;

            SpellHelper.Turn(Caster, mobile);

            var toHeal = Caster.Skills[SkillName.Magery].Value * 0.1;
            toHeal += Utility.Random(1, 5);

            Caster.FireHook(h => h.OnHeal(Caster, mobile, this, ref toHeal));
                
            SpellHelper.Heal((int) toHeal, mobile, Caster);

            mobile.FixedParticles(0x376A, 9, 32, 5005, EffectLayer.Waist);
            mobile.PlaySound(0x1F2);
        }
    }
}