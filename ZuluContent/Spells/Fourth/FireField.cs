using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server.Engines.Magic;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Fourth
{
    public class FireFieldSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public FireFieldSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);

            SpellHelper.Turn(Caster, point);

            Effects.PlaySound(point, Caster.Map, 0x20C);

            var power = 2.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref power));

            var seconds = Caster.Skills[SkillName.Magery].Value / 5.0 + 20.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref seconds));
            var duration = TimeSpan.FromSeconds(seconds);
            
            FieldItem.CreateField(
                (0x398C, 0x3996),
                point,
                Caster,
                duration,
                TimeSpan.FromSeconds(5.0),
                onCreate: item => Effects.SendLocationParticles(item, 0x376A, 9, 10, 5025),
                onTick: item =>
                {
                    var eable = item.GetMobilesInRange(0);
                    foreach (var mobile in eable)
                    {
                        if (mobile.Z + 16 > item.Z
                            && item.Z + 12 > mobile.Z
                            && SpellHelper.ValidIndirectTarget(Caster, mobile)
                            && Caster.CanBeHarmful(mobile, false))
                        {
                            DealDamage(Caster, mobile, power);
                        }
                    }
                    eable.Free();
                },
                onMoveOver: mobile =>
                {
                    DealDamage(Caster, mobile, power);
                    return null;
                } 
            );
            
        }

        private static void DealDamage(Mobile caster, Mobile target, double power)
        {
            if (SpellHelper.CanRevealCaster(target))
                caster.RevealingAction();

            caster.DoHarmful(target);
            SpellHelper.Damage(Utility.Dice((uint)power, 8, 0), target, caster, null, null, ElementalType.Fire);
            target.PlaySound(0x208);
        }
    }
}