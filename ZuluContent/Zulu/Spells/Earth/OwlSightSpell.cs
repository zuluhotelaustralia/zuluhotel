using System;
using System.Collections;
using System.Threading.Tasks;
using Server;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Earth
{
    public class OwlSightSpell : EarthSpell, ITargetableAsyncSpell<Mobile>
    {
        public OwlSightSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            
            var range = Caster.Skills[SkillName.Magery].Value / 10.0;
            var duration = Caster.Skills[SkillName.Magery].Value * 70;
            
            var mobiles = target.GetMobilesInRange((int) range);

            foreach (var mobile in mobiles)
            {
                if (!(mobile is PlayerMobile))
                {
                    continue;
                }
                
                if (!Caster.CanBuff(mobile, true, BuffIcon.NightSight, BuffIcon.Shadow))
                    continue;

                mobile.TryAddBuff(new NightSight
                {
                    Title = "Owl Sight",
                    Icon = BuffIcon.NightSight,
                    Value = LightCycle.DayLevel,
                    Duration = TimeSpan.FromSeconds(duration),
                });
                
                mobile.FixedParticles(0x373A, 10, 10, 5007, EffectLayer.Waist);
                mobile.PlaySound(0x1E3);
            }

            mobiles.Free();
        }
    }
}