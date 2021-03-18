using System;
using System.Threading.Tasks;
using Server.Engines.Magic;
using Server.Targeting;

namespace Server.Spells.Seventh
{
    public class FlameStrikeSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public FlameStrikeSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            
            target.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
            target.PlaySound(0x208);

            var damage = SpellHelper.CalcSpellDamage(Caster, target, this);

            SpellHelper.Damage(damage, target, Caster, this, TimeSpan.Zero, ElementalType.Fire);
        }
    }
}