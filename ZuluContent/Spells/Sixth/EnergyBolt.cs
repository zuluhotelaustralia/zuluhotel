using System;
using System.Threading.Tasks;
using Server.Engines.Magic;
using Server.Targeting;

namespace Server.Spells.Sixth
{
    public class EnergyBoltSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public EnergyBoltSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;

            SpellHelper.Turn(Caster, target);
            
            // Do the effects
            Caster.MovingParticles(target, 0x379F, 7, 0, false, true, 3043, 4043, 0x211);
            Caster.PlaySound(0x20A);

            var damage = SpellHelper.CalcSpellDamage(Caster, target, this);
            SpellHelper.Damage(damage, target, Caster, this, TimeSpan.Zero, ElementalType.Air);
        }
        
        public async Task OnSpellReflected(Mobile target)
        {
            Caster.MovingParticles(target, 0x379F, 7, 0, false, true, 3043, 4043, 0x211);
            await Timer.Pause(500);
            target.MovingParticles(Caster, 0x379F, 7, 0, false, true, 3043, 4043, 0x211);
        }
    }
}