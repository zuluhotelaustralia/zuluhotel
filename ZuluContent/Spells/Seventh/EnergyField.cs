using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Engines.Magic;
using Server.Items;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Seventh
{
    public class EnergyFieldSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public EnergyFieldSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);

            var power = 2.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref power));

            var seconds = Caster.Skills[SkillName.Magery].Value / 5.0 + 20.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref seconds));
            var duration = TimeSpan.FromSeconds(seconds);

            Effects.PlaySound(point, Caster.Map, 0x20B);
            var itemIds = (ewItemId: 0x3946, nsItemId: 0x3956);

            FieldItem.CreateField(
                itemIds,
                point,
                Caster,
                duration,
                TimeSpan.FromSeconds(1.0),
                onCreate: item =>
                {
                    Effects.SendLocationParticles(
                        EffectItem.Create(item.Location, Caster.Map, EffectItem.DefaultDuration),
                        0x376A, 9, 10, 5051
                    );
                },
                onTick: item =>
                {
                    var eastToWest = item.ItemID == itemIds.ewItemId;
                    var eable = Caster.Map.GetMobilesInBounds(
                        // Makes a 1x3 hitbox that extends 1 square either side of the field
                        new Rectangle2D(
                            item.X - (eastToWest ? 0 : 1), 
                            item.Y - (eastToWest ? 1 : 0), 
                            eastToWest ? 1 : 3, 
                            eastToWest ? 3 : 1
                        )
                    );

                    foreach (var mobile in eable)
                    {
                        if (mobile.Z + 16 > item.Z && 
                            item.Z + 12 > mobile.Z && 
                            SpellHelper.ValidIndirectTarget(Caster, mobile) && 
                            Caster.CanBeHarmful(mobile, false)
                        )
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

            var damage = Utility.Dice((uint) power, 8, 0);
            SpellHelper.Damage(damage, target, caster, null, null, ElementalType.Air);
            target.PlaySound(0x208);
        }
    }
}