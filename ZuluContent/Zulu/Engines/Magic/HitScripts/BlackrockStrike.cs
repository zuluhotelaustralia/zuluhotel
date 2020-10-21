using System;
using Server.Spells;
using Server.Spells.Sixth;

namespace Server.Engines.Magic.HitScripts
{
    public class BlackrockStrike : WeaponAbility
    {
        public override void OnHit(Mobile attacker, Mobile defender, ref int damage)
        {
            if (!Validate(attacker))
                return;

            try
            {
                var spell = Spell.Create<DispelSpell>(attacker, null, true);
                spell.Target(attacker, defender);
                
                if (spell.CheckResisted(defender))
                    defender.Mana = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    $"Failed to invoke {GetType().Name} for Mobile: {attacker.GetType().Name}, Serial: {attacker.Serial}");
            }
        }
    }
}