using System.Threading.Tasks;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
#pragma warning disable 1998

namespace Server.Spells.First
{
    public class HealSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public HealSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnCastAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var mobile = response.Target;
            
            if (mobile.Poisoned)
            {
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, Caster == mobile ? 1005000 : 1010398);
                return;
            }

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