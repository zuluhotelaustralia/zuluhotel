using System;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;


namespace Server.Engines.Magic.HitScripts
{
    public class SpellStrike : WeaponAbility
    {
        public Type SpellType { get; protected set; }

        public SpellStrike(Type spellType)
        {
            if (!spellType.IsSubclassOf(typeof(Spell)))
                throw new ArgumentOutOfRangeException($"{nameof(spellType)} {spellType.Name} must inherit from {typeof(Spell)}");
            
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
                    $"Failed to invoke {GetType().Name}<{SpellType.Name}> for Mobile: {attacker.GetType().Name}, Serial: {attacker.Serial}");
            }
        }

        public override bool Validate(Mobile @from)
        {
            return from is BaseCreature && base.Validate(@from);
        }
    }
}