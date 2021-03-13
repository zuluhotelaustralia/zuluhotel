using System;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

#pragma warning disable 1998

namespace Server.Spells.First
{
    public class ReactiveArmorSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public ReactiveArmorSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var target = response.Target;
            
            if (!Caster.CanBuff(target, BuffIcon.ReactiveArmor))
                return;

            var charges = Caster.Skills[SkillName.Magery].Value / 15;
            var duration = Caster.Skills[SkillName.Magery].Value / 5;
            
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref charges));
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref duration));

            target.TryAddBuff(new ReactiveArmor
            {
                Value = (int)charges,
                Duration = TimeSpan.FromSeconds(duration),
            });

            target.FixedParticles(0x376A, 9, 32, 5008, EffectLayer.Waist);
            target.PlaySound(0x1E9);
        }
    }
}