using System;
using Server.Items;
using Server.Spells;
using Server.Spells.Sixth;

namespace Server.Engines.Magic.HitScripts
{
    public class VoidStrike : WeaponAbility
    {
        private static readonly StatType[] Stats = {StatType.Str, StatType.Dex, StatType.Int};

        public override void OnHit(Mobile attacker, Mobile defender, ref int damage)
        {
            if (!Validate(attacker))
                return;

            if (attacker.Weapon != attacker.GetDefaultWeapon() && attacker.Weapon is BaseWeapon)
            {
                var drain = Utility.Random(6) + 10;

                if (drain > 1)
                {
                    damage -= drain;

                    var stat = Stats[Utility.Random(Stats.Length)];
                    switch (stat)
                    {
                        case StatType.Str:
                            defender.Damage(drain, attacker);
                            attacker.Heal(drain);
                            break;
                        case StatType.Dex:
                            defender.Stam -= drain;
                            attacker.Stam += drain;
                            break;
                        case StatType.Int:
                            defender.Mana -= drain;
                            attacker.Mana += drain;
                            break;
                    }
                }
            }
        }
    }
}