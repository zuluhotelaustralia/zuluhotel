using System;
using System.Collections;
using System.Threading.Tasks;
using Server.Items;
using Server.Misc;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Fifth
{
    public class PoisonFieldSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public PoisonFieldSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem)
        {
        }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);
            SpellHelper.Turn(Caster, point);
            

            Effects.PlaySound(point, Caster.Map, 0x20B);
            
            var power = 2.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref power));

            var seconds = Caster.Skills[SkillName.Magery].Value / 5.0 + 20.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref seconds));
            var duration = TimeSpan.FromSeconds(seconds);
            
            FieldItem.CreateField(
                (0x3915, 0x3922),
                point,
                Caster,
                duration,
                TimeSpan.FromSeconds(1.0),
                onCreate: item => Effects.SendLocationParticles(item, 0x376A, 9, 10, 5025),
                onTick: item =>
                {
                    var eable = item.GetMobilesInRange(0);

                    foreach (var m in eable)
                    {
                        if (m.Z + 16 > item.Z && item.Z + 12 > m.Z &&
                            SpellHelper.ValidIndirectTarget(Caster, m) && 
                            Caster.CanBeHarmful(m, false)
                        )
                        {
                            TryPoison(Caster, m, power);
                        }
                    }

                    eable.Free();
                },
                onMoveOver: mobile =>
                {
                    TryPoison(Caster, mobile, power);
                    return null;
                } 
            );
        }

        private static void TryPoison(Mobile caster, Mobile target, double power)
        {
            caster.DoHarmful(target);

            var level = SpellHelper.TryResist(caster, target, SpellCircle.Fifth) 
                ? Utility.Dice(1, (uint)power, 0) 
                : (int)power;
            
            var p = Poison.GetPoison(Math.Min(level, Poison.Poisons.Count - 1));
            target.PlaySound(0x474);
            
            if (target.ApplyPoison(caster, p) == ApplyPoisonResult.Poisoned)
                if (SpellHelper.CanRevealCaster(target))
                    caster.RevealingAction();
            
        }
    }
}