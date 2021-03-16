using System;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Fifth
{
    public class ParalyzeSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public ParalyzeSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            var duration = 2.0 + (Caster.Skills[SkillName.Magery].Value / 25.0);
            target.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref duration));

            Caster.DoHarmful(target);
            target.Paralyze(TimeSpan.FromSeconds(duration));

            target.PlaySound(0x204);
            target.FixedEffect(0x376A, 6, 1);
        }
    }
}