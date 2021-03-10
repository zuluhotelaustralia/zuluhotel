using System;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Second
{
    public class StrengthSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public StrengthSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue || !(response.Target is IBuffable buffable))
                return;

            if (!buffable.BuffManager.CanBuffWithNotifyOnFail<Strength>(Caster))
                return;
            
            var target = response.Target;
            SpellHelper.Turn(Caster, target);

            buffable.BuffManager.AddBuff(new Strength
            {
                Value = SpellHelper.GetModAmount(Caster, target, StatType.Str),
                Duration = SpellHelper.GetDuration(Caster, target),
            });

            target.FixedParticles(0x375A, 10, 15, 5017, EffectLayer.Waist);
            target.PlaySound(0x1EE);
        }
    }
}