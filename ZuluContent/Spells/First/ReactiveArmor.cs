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

            if (!(response.Target is IBuffable buffable))
                return;

            var mobile = response.Target;
            
            if (buffable.BuffManager.HasBuff<ReactiveArmor>())
            {
                Caster.SendLocalizedMessage(Caster == mobile 
                    ? 1005384 // You currently have a reactive armor spell in effect.
                    : 502349 // This target already has Reactive Armor
                ); 
                return;
            }

            var charges = Caster.Skills[SkillName.Magery].Value / 15;
            var duration = Caster.Skills[SkillName.Magery].Value / 5;
            
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref charges));
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref duration));

            buffable.BuffManager.AddBuff(new ReactiveArmor
            {
                Value = (int)charges,
                Start = DateTime.UtcNow,
                Duration = TimeSpan.FromSeconds(duration),
            });

            mobile.FixedParticles(0x376A, 9, 32, 5008, EffectLayer.Waist);
            mobile.PlaySound(0x1E9);
        }
    }
}