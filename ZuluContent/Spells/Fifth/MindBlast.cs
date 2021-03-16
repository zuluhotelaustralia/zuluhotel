using System;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Fifth
{
    public class MindBlastSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public MindBlastSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);

            var casterInt = (double)Caster.Int;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref casterInt));
            
            var targetInt = (double)target.Int;
            target.FireHook(h => h.OnModifyWithMagicEfficiency(target, ref targetInt));

            if (targetInt > casterInt)
            {
                target.FixedEffect(0x374B, 10, 10);
                target.PlaySound(0x1E7);
            }
            else if (Math.Abs(targetInt - casterInt) < 1.0)
            {
                Caster.SendMessage("You are of equal intellect!");
                return;
            }
            
            Caster.FixedParticles(0x374A, 10, 15, 2038, EffectLayer.Head);
            target.FixedParticles(0x374A, 10, 15, 5038, EffectLayer.Head);
            target.PlaySound(0x213);

            SpellHelper.Damage(SpellHelper.CalcSpellDamage(Caster, target, this), target, Caster, this);
        }
    }
}