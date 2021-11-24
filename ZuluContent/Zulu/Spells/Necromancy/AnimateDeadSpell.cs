using System;
using System.Threading.Tasks;
using Scripts.Zulu.Utilities;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Zulu.Spells.Necromancy;

public class AnimateDeadSpell : NecromancerSpell, ITargetableAsyncSpell<Corpse>
{
    public AnimateDeadSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem)
    {
    }

    public async Task OnTargetAsync(ITargetResponse<Corpse> response)
    {
        if (!response.HasValue)
        {
            Caster.SendFailureMessage(1061084); // You cannot animate that.
            return;
        }

        var target = response.Target;
        SpellHelper.Turn(Caster, target);

        var magery = Caster.Skills[SkillName.Magery].Value;
        var duration = magery * 3.0;
        var power = magery - 20.0;
        Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref duration));
        Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref power));


        if (target.Animated || target.IsBones || !(target.Owner is BaseCreature creature) ||
            creature is BaseVendor || creature.IsInvulnerable || power < 1)
        {
            Caster.SendFailureMessage(1061084); // There's not enough life force there to animate.
            return;
        }

        if (power > 95)
            power = 95;

        if (creature is BaseCreatureTemplate bt)
        {
            if (bt.Properties.Resistances.TryGetValue(Info.DamageType, out var value))
            {
                var modifier = 100.0 - value.Min;
                duration = duration * (modifier / 100);
                power = power * modifier / 100;

                if (duration < 1 || power < 1)
                {
                    // TODO: this should be a PrivateOverheadMessage but no overload exists in MUO's Item.cs
                    target.PublicOverheadMessage(MessageType.Regular, 0x3B2, true,
                        "The nature of the target prevents it from being animated!"
                    );
                    return;
                }
            }

            Effects.SendLocationParticles(
                EffectItem.Create(target.Location, target.Map, EffectItem.DefaultDuration),
                0x37BA, 7, 0xa, 0, 3, 9907, 0
            );

            if (Creatures.Create(bt.TemplateName) is BaseCreature summoned)
            {
                var location = SpellHelper.GetSurfaceTop(target.Location);
                target.Delete();

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < summoned.Skills.Length; i++)
                    summoned.Skills[i].BaseFixedPoint = (int)(summoned.Skills[i].BaseFixedPoint * power / 10.0);

                summoned.CreatureType = CreatureType.Undead;

                SpellHelper.Summon(summoned, Caster, 0x22A, TimeSpan.FromSeconds(duration), false);

                if (!summoned.Deleted)
                {
                    summoned.Hue = 1154;
                    summoned.SpellBound = true;

                    summoned.RawStr = (int)(summoned.RawStr * power / 100);
                    summoned.RawInt = (int)(summoned.RawInt * power / 100);
                    summoned.RawDex = (int)(summoned.RawDex * power / 100);

                    summoned.Hits = summoned.HitsMax;
                    summoned.Mana = summoned.ManaMax;
                    summoned.Stam = summoned.StamMax;

                    summoned.Location = location;
                }
            }
        }
    }
}