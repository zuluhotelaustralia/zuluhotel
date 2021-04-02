using System.Threading.Tasks;
using Server.Engines.Magic;
using Server.Targeting;

namespace Server.Spells.Third
{
    public class FireballSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public FireballSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            Caster.MovingParticles(target, 0x36D4, 7, 0, false, true, 9502, 4019, 0x160);
            Caster.PlaySound(0x44B);

            SpellHelper.Damage(SpellHelper.CalcSpellDamage(Caster, target, this), target, Caster, this, null, ElementalType.Fire);
        }
    }
}