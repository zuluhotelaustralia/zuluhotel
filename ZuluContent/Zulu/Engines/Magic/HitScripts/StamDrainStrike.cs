using Server.Items;

namespace Server.Engines.Magic.HitScripts
{
    public class StamDrainStrike : WeaponAbility
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
                    defender.Stam -= drain;
                    attacker.Stam += drain;
                }
            }
        }
    }
}