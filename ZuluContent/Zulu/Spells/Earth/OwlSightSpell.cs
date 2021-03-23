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
    public class OwlSightSpell : EarthSpell, IAsyncSpell
    {
        public override TimeSpan CastDelayBase => TimeSpan.FromSeconds(0);
    
        public override double RequiredSkill => 60.0;
    
        public override int RequiredMana => 5;

        public OwlSightSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public async Task CastAsync()
        {
            var range = Caster.Skills[SkillName.Magery].Value / 10.0;
            var duration = Caster.Skills[SkillName.Magery].Value * 70;
            
            var mobiles = Caster.GetMobilesInRange((int) range);

            foreach (var target in mobiles)
            {
                if (!(target is PlayerMobile))
                {
                    continue;
                }
                
                target.FixedParticles(0x373A, 10, 10, 5007, EffectLayer.Waist);
                target.PlaySound(0x1E3);
                
                if (!target.BeginAction(typeof(LightCycle)))
                {
                    continue;
                }
                
                new LightCycle.OwlSightTimer(target, TimeSpan.FromSeconds(duration)).Start();
                target.LightLevel = 0;
            }

            mobiles.Free();
        }
    }
}