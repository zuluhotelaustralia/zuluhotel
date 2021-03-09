using System;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Third
{
    public class PoisonSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public PoisonSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var target = response.Target;
            
            var level = Caster.Skills[SkillName.Magery].Value / 40 + 1.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref level));

            target.ApplyPoison(Caster, Poison.GetPoison(Math.Min((int)level, Poison.Poisons.Count - 1)));
            target.FixedParticles(0x374A, 10, 15, 5021, EffectLayer.Waist);
            target.PlaySound(0x205);
            target.Spell?.OnCasterHurt();
        }
    }
}