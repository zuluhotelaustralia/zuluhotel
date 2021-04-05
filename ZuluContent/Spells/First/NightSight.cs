using System;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

#pragma warning disable 1998

namespace Server.Spells.First
{
    public class NightSightSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public NightSightSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;

            SpellHelper.Turn(Caster, target);
            
            if (!Caster.CanBuff(target, true, BuffIcon.NightSight, BuffIcon.Shadow))
                return;

            target.TryAddBuff(new NightSight
            {
                Value = LightCycle.DayLevel,
                Duration = TimeSpan.FromSeconds(Caster.Skills.Magery.Value * 60),
            });
            
            target.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
            target.PlaySound(0x1E3);
        }
    }
}