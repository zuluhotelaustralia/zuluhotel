using System;
using System.Threading.Tasks;
using Server;
using Server.Engines.Magic;
using Server.Spells;
using Server.Targeting;

namespace Scripts.Zulu.Spells.TriElemental
{
    public class TriElementalSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public TriElementalSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            
            var damage = SpellHelper.CalcSpellDamage(Caster, target, SpellCircle.Third);

            Caster.MovingParticles(target, 0x36D4, 7, 0, false, true, 9502, 4019, 0x160);
            Caster.PlaySound(0x44B);

            await Timer.Pause(250);
            
            damage += SpellHelper.CalcSpellDamage(Caster, target, SpellCircle.Third);
            
            target.BoltEffect(0);
            
            await Timer.Pause(250);

            damage += SpellHelper.CalcSpellDamage(Caster, target, SpellCircle.Third);

            target.FixedParticles(0x3789, 30, 30, 5028, EffectLayer.Waist);
            target.PlaySound(0x0116);
            target.PlaySound(0x0117);
            
            SpellHelper.Damage(damage, target, Caster, this, TimeSpan.Zero, ElementalType.None);
        }
    }
}