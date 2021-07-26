using System;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

#pragma warning disable 1998

namespace Server.Spells.First
{
    public class ClumsySpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public ClumsySpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var target = response.Target;

            if (!Caster.CanBuff(target, true, BuffIcon.Bless, BuffIcon.Agility, BuffIcon.Resilience, BuffIcon.Clumsy))
                return;
            
            target.TryAddBuff(new StatBuff(StatType.Dex)
            {
                Value = SpellHelper.GetModAmount(Caster, target, StatType.Dex) * -1,
                Duration = SpellHelper.GetDuration(Caster, target),
            });
            
            SpellHelper.Turn(Caster, target);

            target.Spell?.OnCasterHurt();
            target.Paralyzed = false;

            target.FixedParticles(0x3779, 10, 15, 5002, EffectLayer.Head);
            target.PlaySound(0x1DF);
        }
    }
}