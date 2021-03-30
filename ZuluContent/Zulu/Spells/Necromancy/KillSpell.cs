using System.Threading.Tasks;
using Server.Targeting;
using Server;
using Server.Mobiles;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic;
using static Server.Engines.Magic.IElementalResistible;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class KillSpell : NecromancerSpell, ITargetableAsyncSpell<Mobile>
    {
        public KillSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            SpellHelper.Turn(Caster, target);

            var multiplier = target is BaseCreature ? 2.0 : 1.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref multiplier));

            var damage = SpellHelper.CalcSpellDamage(Caster, target, this);
            var protection = (int)GetProtectionLevelForResist(target.GetResist(Info.DamageType));
            var instantKill = (int)(Caster.Skills.Magery.Value * multiplier / 3);

            if (protection > 0)
            {
                instantKill -= (int) (instantKill * protection * 0.25);
                if (instantKill < 1)
                    instantKill = 1;
            }
            
            target.FixedParticles(0x375B, 1, 16, 5044, EffectLayer.Waist);
            target.PlaySound(0x201);

            if (target.Hits <= instantKill)
            {
                target.Damage(target.HitsMax); // Raw damage
            }
            else if(target.Hits <= instantKill * 1.5)
            {
                var victimSkill = target.Skills.MagicResist.Value;
                var resistChance = (int) (victimSkill / multiplier) - (Caster.Skills.EvalInt.Value / 3);
                if (Utility.RandomMinMax(1, 100) <= resistChance)
                    SpellHelper.Damage(damage, target, Caster, this);
                else
                    target.Damage(target.HitsMax); // Raw damage
            }
            else
            {
                SpellHelper.Damage(damage, target, Caster, this);
            }
            
            
        }
    }
}