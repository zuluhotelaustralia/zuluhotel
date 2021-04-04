using System;
using System.Collections;
using System.Threading.Tasks;
using Server;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;

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
                
                mobile.FixedParticles(0x373A, 10, 10, 5007, EffectLayer.Waist);
                mobile.PlaySound(0x1E3);
                
                if (!mobile.BeginAction(typeof(LightCycle)))
                {
                    continue;
                }
                
                new LightCycle.OwlSightTimer(mobile, TimeSpan.FromSeconds(duration)).Start();
                mobile.LightLevel = 0;
            }

            mobiles.Free();
        }
    }
}