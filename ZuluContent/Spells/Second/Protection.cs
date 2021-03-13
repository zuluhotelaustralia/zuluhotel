using System;
using System.Threading.Tasks;
using Server.Spells.First;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Second
{
    public class ProtectionSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public ProtectionSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            if (!Caster.CanBuff(target, BuffIcon.Bless))
                return;
            
            target.TryAddBuff(new Protection
            {
                Value = (int)(SpellHelper.GetModAmount(Caster, target) / 2.0),
                Duration = SpellHelper.GetDuration(Caster, target),
            });
            
            SpellHelper.Turn(Caster, target);

            target.FixedParticles(0x373B, 9, 20, 5027, EffectLayer.Waist);
            target.PlaySound(0x1ED);
        }
    }
}