using System;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;

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

            var mobile = response.Target;
            
            if (!mobile.BeginAction(typeof(ReactiveArmorSpell)))
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

            if (Caster is IEnchanted enchanted)
            {
                enchanted.Enchantments.Set((ReactiveArmor e) => e.Value = (int) charges);
            }

            Caster.FixedParticles(0x376A, 9, 32, 5008, EffectLayer.Waist);
            Caster.PlaySound(0x1F2);

            EndActionAsync(mobile, duration);
        }

        private static async void EndActionAsync(Mobile mobile, double duration)
        {
            await Timer.Pause(TimeSpan.FromSeconds(duration));

            if (!mobile.CanBeginAction<ReactiveArmorSpell>())
            {
                mobile.SendLocalizedMessage(1005556); // Your reactive armor spell has been nullified.
                mobile.EndAction<ReactiveArmorSpell>();
            }
        }
    }
}