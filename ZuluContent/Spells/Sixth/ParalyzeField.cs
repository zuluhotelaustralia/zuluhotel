using System;
using System.Threading.Tasks;
using Server.Items;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Sixth
{
    public class ParalyzeFieldSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public ParalyzeFieldSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
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
                (0x3967, 0x3979),
                point,
                Caster,
                duration,
                TimeSpan.Zero,
                onCreate: item => Effects.SendLocationParticles(
                    EffectItem.Create(item.Location, Caster.Map, EffectItem.DefaultDuration),
                    0x376A,
                    9,
                    10,
                    5048
                ),
                onMoveOver: mobile =>
                {
                    if (SpellHelper.ValidIndirectTarget(Caster, mobile) && Caster.CanBeHarmful(mobile, false))
                    {
                        if (SpellHelper.CanRevealCaster(mobile))
                            Caster.RevealingAction();

                        Caster.DoHarmful(mobile);

                        var paralyzeDuration = 10.0;
                        Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref paralyzeDuration));

                        if (!SpellHelper.TryResist(Caster, mobile, SpellCircle.Fifth))
                        {
                            mobile.Paralyze(TimeSpan.FromSeconds(paralyzeDuration));
                            mobile.PlaySound(0x204);
                            mobile.FixedEffect(0x376A, 10, 16);
                        }
                    }

                    return true;
                }
            );
        }
    }
}