using System;
using Server.Spells;
using Scripts.Zulu.Spells.Earth;
using Server.Spells.Fourth;
using Server.Spells.Third;

namespace Server.Engines.Magic.HitScripts
{
    public class TriElementalStrike : WeaponAbility
    {
        private static readonly Action<Mobile, Mobile>[] Spells =
        {
            (attacker, defender) => Spell.Create<LightningSpell>(attacker, null, true).Target(defender),
            (attacker, defender) => Spell.Create<FireballSpell>(attacker, null, true).Target(defender),
            (attacker, defender) => Spell.Create<IceStrikeSpell>(attacker, null, true).Target(defender)
        };

        public override void OnHit(Mobile attacker, Mobile defender, ref int damage)
        {
            if (!Validate(attacker))
                return;

            try
            {
                foreach (var castAction in Spells) 
                    castAction(attacker, defender);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    $"Failed to invoke {GetType().Name}> for Creature: {attacker.GetType().Name}, Serial: {attacker.Serial}");
            }
        }
    }
}