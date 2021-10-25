using System;
using Server.Spells;

namespace Server.Engines.Magic.HitScripts
{
    public class TriElementalStrike : WeaponAbility
    {
        public override void OnHit(Mobile attacker, Mobile defender, ref int damage)
        {
            if (!Validate(attacker))
                return;

            Cast(attacker, defender);
        }
        
        private static async void Cast(Mobile caster, Mobile defender)
        {
            var damage = SpellHelper.CalcSpellDamage(caster, defender, 3);

            caster.MovingParticles(defender, 0x36D4, 7, 0, false, true, 9502, 4019, 0x160);
            caster.PlaySound(0x44B);

            await Timer.Pause(250);
            
            damage += SpellHelper.CalcSpellDamage(caster, defender, 3);
            
            defender.BoltEffect(0);
            
            await Timer.Pause(250);

            damage += SpellHelper.CalcSpellDamage(caster, defender, 3);

            defender.FixedParticles(0x3789, 30, 30, 5028, EffectLayer.Waist);
            defender.PlaySound(0x0116);
            defender.PlaySound(0x0117);
            
            SpellHelper.Damage(damage, defender, caster, null, TimeSpan.Zero, ElementalType.None);
        }
    }
}