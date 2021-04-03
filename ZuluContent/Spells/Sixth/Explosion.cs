using System;
using System.Threading.Tasks;
using Server.Engines.Magic;
using Server.Targeting;

namespace Server.Spells.Sixth
{
    public class ExplosionSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public ExplosionSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;

            SpellHelper.Turn(Caster, target);
            
            target.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
            target.PlaySound(0x307);

            var damage = SpellHelper.CalcSpellDamage(Caster, target, this);
            SpellHelper.Damage(damage, target, Caster, this);
        }
    }
}