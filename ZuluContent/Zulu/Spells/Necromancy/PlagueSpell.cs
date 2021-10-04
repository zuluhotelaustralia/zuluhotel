using System;
using System.Threading.Tasks;
using Server;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class PlagueSpell : NecromancerSpell, ITargetableAsyncSpell<IPoint3D>
    {
        public PlagueSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);
            SpellHelper.Turn(Caster, point);

            var magery = Caster.Skills.Magery.Value;
            var range = magery / 20.0;
            var plvl = magery / 50.0 + 2.0;
            
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref range));
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref plvl));

            var poison = Poison.GetPoison(Math.Min((int) plvl, Poison.Poisons.Count - 1));
            
            var eable = Caster.Map.GetMobilesInRange(point, (int) range);
            foreach (var mobile in eable)
            {
                if (Caster == mobile || 
                    !SpellHelper.ValidIndirectTarget(Caster, mobile) ||
                    !Caster.CanBeHarmful(mobile, false) || 
                    !Caster.InLOS(mobile) ||
                    mobile.CheckPoisonImmunity(Caster, poison))
                {
                    continue;
                }

                var level = SpellHelper.TryResistDamage(Caster, mobile, Circle, poison.Level);
                
                if (level > 0) 
                    mobile.ApplyPoison(Caster, Poison.GetPoison(level));

                mobile.FixedParticles(0x3914, 8, 16, 5021, EffectLayer.Waist);
                mobile.PlaySound(0x1E1);
                
                mobile.Spell?.OnCasterHurt();
            }
            eable.Free();
        }
    }
}