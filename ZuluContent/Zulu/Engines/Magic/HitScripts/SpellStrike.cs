using System;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;


namespace Server.Engines.Magic.HitScripts
{
    public class SpellStrike : WeaponAbility
    {
        public readonly SpellEntry SpellType;

        public SpellStrike(SpellEntry spellType)
        {
            SpellType = spellType;
        }
        
        public override void OnHit(Mobile attacker, Mobile defender, ref int damage)
        {
            if (!Validate(attacker))
                return;
            try
            {
                switch (SpellRegistry.Create(SpellType, attacker))
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
                    $"Failed to invoke {GetType().Name}<{SpellType}> for Mobile: {attacker.GetType().Name}, Serial: {attacker.Serial}");
            }
        }

        public override bool Validate(Mobile @from)
        {
            return from is BaseCreature && base.Validate(@from);
        }
    }
}