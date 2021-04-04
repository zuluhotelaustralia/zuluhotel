using System;
using Server.Spells;
using Server.Spells.Sixth;
using Server.Targeting;

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
                var spell = SpellRegistry.Create<DispelSpell>(attacker);
                
                spell.OnTargetAsync(new TargetResponse<Mobile>
                {
                    Target = defender,
                }).ConfigureAwait(false);
                
                if (SpellHelper.TryResist(attacker, defender, spell.Circle))
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