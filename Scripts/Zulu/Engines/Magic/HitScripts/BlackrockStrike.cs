using System;
using Server.Spells;
using Server.Spells.Sixth;

namespace Server.Engines.Magic.HitScripts
{
    public class BlackrockStrike : WeaponAbility
    {
        public override void OnHit(Mobile attacker, Mobile defender, double damage)
        {
            if (!Validate(attacker))
                return;
            
            try
            {
                var spell = Spell.Create<DispelSpell>(attacker, null, true);
                
                var casted = spell.Cast();
                attacker.Target?.Invoke(attacker, defender);

                if (casted && spell.CheckResisted(defender))
                    defender.Mana = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to invoke {GetType().Name} for Creature: {attacker.GetType().Name}, Serial: {attacker.Serial}");
            }
        }
    }
}