
using System;
using Server;
using Server.Mobiles;
using Server.Spells;


namespace Server.Engines.Magic.HitScripts
{
    public class SpellStrike<T> : WeaponAbility where T : Spell
    {
        public override void OnHit(Mobile attacker, Mobile defender, double damage)
        {
            if (!Validate(attacker))
                return;
            try
            {
                var spell = Spell.Create<T>(attacker, null, true);
                
                spell?.Cast();
                attacker.Target?.Invoke(attacker, defender);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to invoke {GetType().Name}<{typeof(T).Name}> for Creature: {attacker.GetType().Name}, Serial: {attacker.Serial}");
            }
        }
        
        public Type GetSpellType()
        {
            return typeof(T);
        }
    }
}