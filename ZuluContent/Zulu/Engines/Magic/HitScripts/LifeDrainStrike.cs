using System;
using Server.Items;
using Server.Spells;
using Server.Spells.Sixth;

namespace Server.Engines.Magic.HitScripts
{
    public class LifeDrainStrike : WeaponAbility
    {
        public override void OnHit(Mobile attacker, Mobile defender, ref int damage)
        {
            if (!Validate(attacker))
                return;

            if (attacker.Weapon != attacker.GetDefaultWeapon() && attacker.Weapon is BaseMeleeWeapon)
            {
                var drain = (int)(damage * 0.25);

                if (drain > 1)
                {
                    attacker.Heal(drain);
                    damage -= drain;
                }
            }
        }
    }
}