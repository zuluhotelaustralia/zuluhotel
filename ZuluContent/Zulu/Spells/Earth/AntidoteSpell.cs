using System;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server;
using Server.Engines.Magic;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Earth
{
    public class AntidoteSpell : EarthSpell, ITargetableAsyncSpell<Mobile>
    {
        public AntidoteSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);

            if (target.CurePoison(Caster))
            {
                if (Caster != target)
                {
                    Caster.SendLocalizedMessage(1010058); // You have cured the target of all poisons!
                    target.SendLocalizedMessage(1010059);  // You have been cured of all poisons.
                }
                else
                {
                    Caster.SendLocalizedMessage(1010059);  // You have been cured of all poisons.
                }

                if (!Caster.CanBuff(target, icons: BuffIcon.PoisonImmunity))
                    return;
                    
                var power = Caster.Skills[SkillName.Magery].Value / 25;
                Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref power));
                    
                var duration = Caster.Skills[SkillName.Magery].Value * 2;
                Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref duration));

                power = Math.Min(power, 5);
                    
                target.TryAddBuff(new PoisonImmunity
                {
                    Value = (PoisonLevel) power,
                    Duration = TimeSpan.FromSeconds(duration),
                });
            }
            else
            {
                target.SendLocalizedMessage(1010060); // You have failed to cure your target!
            }

            target.FixedParticles(0x373A, 10, 15, 5012, EffectLayer.Waist);
            target.PlaySound(0x1E0);
        }
    }
}