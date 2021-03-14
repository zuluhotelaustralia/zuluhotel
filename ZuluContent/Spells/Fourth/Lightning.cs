using System;
using System.Threading.Tasks;
using Server.Engines.Magic;
using Server.Targeting;

namespace Server.Spells.Fourth
{
    public class LightningSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public LightningSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            
            var damage = SpellHelper.CalcSpellDamage(Caster, target, this);
            target.BoltEffect(0);

            SpellHelper.Damage(damage, target, Caster, this, TimeSpan.Zero, ElementalType.Air);
        }
    }
}