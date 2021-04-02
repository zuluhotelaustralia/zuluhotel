using System;
using System.Threading.Tasks;
using Server.Mobiles;
using Scripts.Zulu.Engines.Classes;
using Server;
using Server.Engines.Magic;
using Server.Multis;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Zulu.Spells.Earth
{
    public class RisingFireSpell : EarthSpell, ITargetableAsyncSpell<Mobile>
    {
        public override TimeSpan CastDelayBase => TimeSpan.FromSeconds(0);
    
        public override double RequiredSkill => 60.0;
    
        public override int RequiredMana => 5;

        public RisingFireSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        private async void RaiseFire(Mobile target)
        {
            target.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);

            await Timer.Pause(250);
            
            target.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
            target.PlaySound(0x208);
            
            await Timer.Pause(250);
            
            target.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
            target.PlaySound(0x208);

            var damage = SpellHelper.CalcSpellDamage(Caster, target, this);
            SpellHelper.Damage(damage / 2, target, Caster, this);
        }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            
            var range = 3.0;
            
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref range));

            var mobiles = target.GetMobilesInRange((int) range);
            
            var casterHouse = BaseHouse.FindHouseAt(Caster);

            foreach (var mobile in mobiles)
            {
                if (!SpellHelper.ValidIndirectTarget(Caster, target) ||
                    !Caster.CanBeHarmful(target, false)
                )
                    continue;

                if (BaseHouse.FindHouseAt(target) is { } targetHouse && 
                    casterHouse == targetHouse &&
                    Caster.Location.Z != target.Location.Z && 
                    target.Location.Z >= Caster.Location.Z + 10
                )
                    continue;
                
                RaiseFire(mobile);
            }

            mobiles.Free();
            
            await Timer.Pause(1000);

            mobiles = Caster.GetMobilesInRange(3);
            
            foreach (var mobile in mobiles)
            {
                if (!SpellHelper.ValidIndirectTarget(Caster, target) ||
                    !Caster.CanBeHarmful(target, false)
                )
                    continue;

                if (BaseHouse.FindHouseAt(target) is { } targetHouse && 
                    casterHouse == targetHouse &&
                    Caster.Location.Z != target.Location.Z && 
                    target.Location.Z >= Caster.Location.Z + 10
                )
                    continue;
                
                RaiseFire(mobile);
            }
            
            mobiles.Free();
        }
    }
}