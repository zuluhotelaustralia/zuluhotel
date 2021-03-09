using System;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

#pragma warning disable 1998

namespace Server.Spells.First
{
    public class ClumsySpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public ClumsySpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            if (!(response.Target is IBuffable buffable))
                return;
            
            var target = response.Target;

            if (buffable.BuffManager.HasBuff<Agility>())
            {
                Caster.SendLocalizedMessage(Caster == target 
                    ? 502173 // You are already under a similar effect.
                    : 1156094 // Your target is already under the effect of this ability.
                ); 
                return;
            }
            
            SpellHelper.Turn(Caster, target);
            
            buffable.BuffManager.AddBuff(new Agility
            {
                Value = SpellHelper.GetModAmount(Caster, target, StatType.Dex) * -1,
                Start = DateTime.UtcNow,
                Duration = SpellHelper.GetDuration(Caster, target),
            });
            
            target.Spell?.OnCasterHurt();
            target.Paralyzed = false;

            target.FixedParticles(0x3779, 10, 15, 5002, EffectLayer.Head);
            target.PlaySound(0x1DF);
        }
    }
}