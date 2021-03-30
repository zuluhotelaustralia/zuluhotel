using System.Threading.Tasks;
using Server;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class SpectresTouchSpell : NecromancerSpell, ITargetableAsyncSpell<IPoint3D>
    {
        public SpectresTouchSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);
            SpellHelper.Turn(Caster, point);

            var range = Caster.Skills.Magery.Value / 30.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref range));

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

                var damage = SpellHelper.CalcSpellDamage(Caster, mobile, this, true);
                
                SpellHelper.Damage(damage, mobile, Caster, this);
                mobile.FixedParticles(0x37C4, 10, 15, 5013, EffectLayer.Waist);
                mobile.PlaySound(0x1F1);
            }
            
            eable.Free();
        }
    }
}