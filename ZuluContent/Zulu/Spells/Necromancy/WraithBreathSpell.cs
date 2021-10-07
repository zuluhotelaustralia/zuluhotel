using System;
using System.Threading.Tasks;
using Server;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class WraithBreathSpell : NecromancerSpell, ITargetableAsyncSpell<IPoint3D>
    {
        public WraithBreathSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);
            SpellHelper.Turn(Caster, point);
            
            var magery = Caster.Skills.Magery.Value;
            var range = magery / 30.0;
            var durationSeconds = 15.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref range));
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref durationSeconds));

            var duration = TimeSpan.FromSeconds(durationSeconds);
            
            var eable = Caster.Map.GetMobilesInRange(point, (int) range);
            foreach (var mobile in eable)
            {
                if (Caster == mobile || 
                    !SpellHelper.ValidIndirectTarget(Caster, mobile) ||
                    !Caster.CanBeHarmful(mobile, false) || 
                    !Caster.InLOS(mobile))
                {
                    continue;
                }
                
                mobile.Paralyze(duration);
                Caster.DoHarmful(mobile, true);
                mobile.FixedParticles(0x374B, 7, 16, 5013, EffectLayer.Waist);
                mobile.PlaySound(0x1F9);
            }
            eable.Free();
        }
    }
}