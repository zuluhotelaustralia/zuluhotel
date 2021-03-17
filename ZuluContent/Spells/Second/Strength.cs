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
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            if (!Caster.CanBuff(target, icons: BuffIcon.Strength))
                return;
            
            target.TryAddBuff(new StatBuff(StatType.Str)
            {
                Value = SpellHelper.GetModAmount(Caster, target, StatType.Str),
                Duration = SpellHelper.GetDuration(Caster, target),
            });
            SpellHelper.Turn(Caster, target);

            target.FixedParticles(0x375A, 10, 15, 5017, EffectLayer.Waist);
            target.PlaySound(0x1EE);
        }
    }
}