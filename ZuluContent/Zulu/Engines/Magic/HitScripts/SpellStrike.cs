using System;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;


namespace Server.Engines.Magic.HitScripts
{
    public class SpellStrike<T> : WeaponAbility where T : Spell
    {
        public override void OnHit(Mobile attacker, Mobile defender, ref int damage)
        {
            if (!Validate(attacker))
                return;
            try
            {
                switch (SpellRegistry.Create<T>(attacker))
                {
                    case ITargetableAsyncSpell<Mobile> targetableAsyncSpell:
                        targetableAsyncSpell.OnTargetAsync(new TargetResponse<Mobile>
                        {
                            Target = defender,
                            Type = TargetResponseType.Success
                        });
                        break;
                    case IAsyncSpell asyncSpell:
                        asyncSpell.CastAsync();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    $"Failed to invoke {GetType().Name}<{typeof(T).Name}> for Mobile: {attacker.GetType().Name}, Serial: {attacker.Serial}");
            }
        }

        public Type GetSpellType()
        {
            return typeof(T);
        }

        public override bool Validate(Mobile @from)
        {
            return from is BaseCreature && base.Validate(@from);
        }
    }
}