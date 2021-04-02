using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Engines.Magic;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Seventh
{
    public class MeteorSwarmSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public MeteorSwarmSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);

            var range = 3.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref range));

            var eable = Caster.Map.GetMobilesInRange(point, (int) range);

            var targets = eable.Where(target =>
                SpellHelper.ValidIndirectTarget(Caster, target) && 
                Caster.CanBeHarmful(target, false) &&
                Caster.InLOS(target)
            ).ToList();
            
            eable.Free();
            
            targets.ForEach(DealDamage);
            await Timer.Pause(1000);
            targets.ForEach(DealDamage);
        }

        private void DealDamage(Mobile target)
        {
            var damage = SpellHelper.CalcSpellDamage(Caster, target, this, true);
            
            SpellHelper.Damage(damage, target, Caster, this);
            Effects.PlaySound(Caster.Location, Caster.Map, 0x160);
            
            Caster.MovingParticles(target, 0x36D4, 7, 0, false, true, 9501, 1, 0, 0x100);
        }
    }
}