using System;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Sixth;

namespace Server.Engines.Magic.HitScripts
{
    public class BanishStrike : WeaponAbility
    {
        public override void OnHit(Mobile attacker, Mobile defender, ref int damage)
        {
            if (!Validate(attacker))
                return;

            if (defender is BaseCreature bc && bc.IsDispellable)
            {
                Effects.SendLocationParticles(
                    EffectItem.Create(defender.Location, defender.Map, EffectItem.DefaultDuration), 0x3728, 8, 20,
                    5042);
                Effects.PlaySound(defender, 0x201);

                defender.Delete();
            }
        }
    }
}