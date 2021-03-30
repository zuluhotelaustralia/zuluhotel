using System;
using System.Threading.Tasks;
using Server;
using Server.Targeting;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class WyvernStrikeSpell : NecromancerSpell, ITargetableAsyncSpell<Mobile>
    {
        public WyvernStrikeSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            SpellHelper.Turn(Caster, target);


            var damage = SpellHelper.CalcSpellDamage(Caster, target, this);
            var magery = Caster.Skills.Magery.Value;
            var plvl = magery / 40.0 + 1.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref plvl));
            var max = 4.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref max));

            plvl = SpellHelper.TryResistDamage(Caster, target, Circle, (int)plvl);

            if (plvl > max)
                plvl = max;

            var protection = target.GetResist(Info.DamageType);
            if (protection > 0)
            {
                plvl -= (int) (plvl * protection);
                if (plvl < 1)
                    plvl = 0;
            }

            SpellHelper.Damage(damage, target, Caster, this);
            if (plvl > 0) 
                target.ApplyPoison(Caster, Poison.GetPoison(Math.Min((int) plvl, Poison.Poisons.Count - 1)));
        }
    }
}